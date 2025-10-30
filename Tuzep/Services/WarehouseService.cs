using Tuzep.Core.Model.MaterialModels;
using Tuzep.Data.Repository;
using static Tuzep.Data.Repository.WarehouseRepository;

namespace Tuzep.Services
{
    /// <summary>
    /// Provides high-level operations for managing warehouses, materials, and their quantities.
    /// This service coordinates interactions between repositories and logs all material movements.
    /// </summary>
    public class WarehouseService
    {
        private readonly MaterialRepository _materialRepo;
        private readonly WarehouseRepository _warehouseRepo;
        private readonly string _logFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseService"/> class.
        /// </summary>
        /// <param name="materialRepo">Repository for managing material records.</param>
        /// <param name="warehouseRepo">Repository for managing warehouse data and inventory.</param>
        public WarehouseService(MaterialRepository materialRepo, WarehouseRepository warehouseRepo)
        {
            _materialRepo = materialRepo;
            _warehouseRepo = warehouseRepo;

            // Ensure log directory exists
            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDir);
            _logFilePath = Path.Combine(logDir, "MaterialMovement.log");
        }

        #region MATERIAL & WAREHOUSE ACCESSORS

        /// <summary>
        /// Retrieves all materials from the repository.
        /// </summary>
        public List<Material> GetAllMaterials() => _materialRepo.GetAll();

        /// <summary>
        /// Retrieves a material by its unique ID.
        /// </summary>
        /// <param name="id">The material ID.</param>
        /// <returns>The matching <see cref="Material"/> instance, or null if not found.</returns>
        public Material? GetMaterialById(int id) => _materialRepo.GetById(id);

        /// <summary>
        /// Retrieves all warehouses registered in the database.
        /// </summary>
        public List<WarehouseDTO> GetAllWarehouses() => _warehouseRepo.GetAllWarehouses();

        /// <summary>
        /// Retrieves the contents of a specific warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse ID.</param>
        /// <returns>A list of materials and their associated quantities.</returns>
        public List<(Material Material, int Quantity)> GetWarehouseContent(int warehouseId)
            => _warehouseRepo.GetWarehouseContent(warehouseId);

        #endregion

        #region FILTERING

        /// <summary>
        /// Filters materials based on multiple optional criteria.
        /// </summary>
        /// <param name="source">The source list of materials.</param>
        /// <param name="nameFilter">Optional name substring to search for.</param>
        /// <param name="minPrice">Optional minimum price threshold.</param>
        /// <param name="maxPrice">Optional maximum price threshold.</param>
        /// <param name="typeFilter">Optional material type to match.</param>
        /// <returns>A filtered list of materials.</returns>
        public List<Material> FilterMaterials(
            IEnumerable<Material> source,
            string? nameFilter = null,
            double? minPrice = null,
            double? maxPrice = null,
            MaterialTypes? typeFilter = null)
        {
            var query = source.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
                query = query.Where(m => m.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                query = query.Where(m => m.UnitPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(m => m.UnitPrice <= maxPrice.Value);

            if (typeFilter.HasValue)
                query = query.Where(m => m.MaterialType == typeFilter.Value);

            return query.ToList();
        }

        #endregion

        #region WAREHOUSE VALUE

        /// <summary>
        /// Calculates the total monetary value of a warehouse based on its materials.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to evaluate.</param>
        /// <returns>The total value in monetary units.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the value calculation fails.</exception>
        public double GetTotalWarehouseValue(int warehouseId)
        {
            try
            {
                return (double)_warehouseRepo.GetTotalWarehouseValue(warehouseId);
            }
            catch (Exception ex)
            {
                LogError("VALUE", warehouseId, ex);
                throw new InvalidOperationException("Failed to calculate warehouse value.", ex);
            }
        }

        #endregion

        #region MATERIAL MANAGEMENT

        /// <summary>
        /// Updates an existing material record in the database.
        /// </summary>
        /// <param name="material">The updated material object.</param>
        public void UpdateMaterial(Material material)
        {
            _materialRepo.Update(material);
        }

        /// <summary>
        /// Updates the quantity of a material stored in a specific warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <param name="materialId">The ID of the material.</param>
        /// <param name="newQuantity">The new quantity to set.</param>
        /// <exception cref="ArgumentException">Thrown if the new quantity is negative.</exception>
        public void UpdateQuantityInWarehouse(int warehouseId, int materialId, int newQuantity)
        {
            ValidateQuantity(newQuantity);
            _warehouseRepo.UpdateMaterialQuantityInWarehouse(warehouseId, materialId, newQuantity);
            LogMovement("SET_QUANTITY", warehouseId, materialId, newQuantity);
        }

        /// <summary>
        /// Adds a new material to a warehouse or updates the quantity if it already exists.
        /// </summary>
        /// <param name="newMaterial">The material to add or update.</param>
        /// <param name="warehouseId">The target warehouse ID.</param>
        /// <param name="quantity">The quantity to add or set.</param>
        /// <exception cref="InvalidOperationException">Thrown if the material insert operation fails.</exception>
        public void AddOrUpdateMaterialInWarehouse(Material newMaterial, int warehouseId, int quantity)
        {
            ValidateQuantity(quantity);

            var existingMaterial = _materialRepo.GetBySpecification(newMaterial);

            if (existingMaterial != null)
            {
                _warehouseRepo.AddMaterialToWarehouse(warehouseId, existingMaterial.Id, quantity);
                LogMovement("UPDATE_EXISTING", warehouseId, existingMaterial.Id, quantity);
            }
            else
            {
                _materialRepo.Insert(newMaterial);

                var inserted = _materialRepo.GetBySpecification(newMaterial);
                if (inserted == null)
                    throw new InvalidOperationException("Failed to insert new material.");

                _warehouseRepo.AddMaterialToWarehouse(warehouseId, inserted.Id, quantity);
                LogMovement("ADD_NEW", warehouseId, inserted.Id, quantity);
            }
        }

        /// <summary>
        /// Removes a specified quantity of material from a warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse ID.</param>
        /// <param name="materialId">The material ID.</param>
        /// <param name="quantity">The quantity to remove.</param>
        public void RemoveMaterial(int warehouseId, int materialId, int quantity)
        {
            ValidateQuantity(quantity);
            _warehouseRepo.RemoveMaterialFromWarehouse(warehouseId, materialId, quantity);
            LogMovement("REMOVE", warehouseId, materialId, quantity);
        }

        #endregion

        #region VALIDATION & LOGGING

        /// <summary>
        /// Validates that a quantity value is non-negative.
        /// </summary>
        /// <param name="quantity">The quantity value to validate.</param>
        /// <exception cref="ArgumentException">Thrown if the quantity is negative.</exception>
        private static void ValidateQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity must be greater than zero.");
        }

        /// <summary>
        /// Writes a material movement record to the log file.
        /// </summary>
        private void LogMovement(string action, int warehouseId, int materialId, int quantity)
        {
            try
            {
                var material = _materialRepo.GetById(materialId);
                string materialName = material?.Name ?? "Unknown";

                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                              $"Warehouse={warehouseId}, Action={action}, " +
                              $"Material={materialName}, Quantity={quantity}";

                File.AppendAllText(_logFilePath, line + Environment.NewLine);
            }
            catch (Exception ex)
            {
                LogError(action, warehouseId, ex);
            }
        }

        /// <summary>
        /// Writes an error entry to the log file.
        /// </summary>
        private void LogError(string action, int warehouseId, Exception ex)
        {
            string line = $"[ERROR] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                          $"Warehouse={warehouseId}, Action={action}, Message={ex.Message}";
            File.AppendAllText(_logFilePath, line + Environment.NewLine);
        }

        #endregion

        #region IMPORT FROM CSV

        /// <summary>
        /// Imports a material definition from a CSV file and adds it to the warehouse.
        /// </summary>
        /// <remarks>
        /// By default, materials are imported into the first warehouse (ID=1) with a default quantity of 1.
        /// </remarks>
        public void ImportMaterialFromCsv()
        {
            try
            {
                var importedMaterial = CsvService.ImportCSV();
                AddOrUpdateMaterialInWarehouse(importedMaterial, 1, 1);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to import material from CSV.");
                return;
            }

            MessageBox.Show("Material imported successfully.");
        }

        #endregion
    }
}
