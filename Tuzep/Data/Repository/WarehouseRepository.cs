using MySql.Data.MySqlClient;
using Tuzep.Core.Model.MaterialModels;
using Tuzep.Data.Helpers;

namespace Tuzep.Data.Repository
{
    /// <summary>
    /// Provides database operations for managing warehouse data, including
    /// warehouse retrieval, material inventory management, and total warehouse valuation.
    /// </summary>
    /// <remarks>
    /// This repository works directly with the MySQL database using the <see cref="DatabaseManager"/>
    /// and collaborates with the <see cref="MaterialRepository"/> to manage material records
    /// when warehouse content changes.
    /// </remarks>
    public class WarehouseRepository
    {
        private readonly DatabaseManager _dbManager;
        private readonly MaterialRepository _materialRepo;

        /// <summary>
        /// Represents a lightweight data transfer object (DTO) for basic warehouse information.
        /// </summary>
        public struct WarehouseDTO
        {
            /// <summary>
            /// Gets or sets the unique identifier of the warehouse.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the display name of the warehouse.
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseRepository"/> class.
        /// </summary>
        /// <param name="dbManager">Provides database connection and management services.</param>
        /// <param name="materialRepo">Repository for accessing and managing material data.</param>
        public WarehouseRepository(DatabaseManager dbManager, MaterialRepository materialRepo)
        {
            _dbManager = dbManager;
            _materialRepo = materialRepo;
        }

        /// <summary>
        /// Retrieves all available warehouses from the database.
        /// </summary>
        /// <returns>A list of <see cref="WarehouseDTO"/> objects containing warehouse IDs and names.</returns>
        public List<WarehouseDTO> GetAllWarehouses()
        {
            const string query = "SELECT id, name FROM warehouses;";
            var result = new List<WarehouseDTO>();

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new WarehouseDTO
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name")
                });
            }

            return result;
        }

        /// <summary>
        /// Retrieves all materials and their quantities from a specific warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to query.</param>
        /// <returns>
        /// A list of tuples containing <see cref="Material"/> objects and their associated quantities.
        /// </returns>
        public List<(Material Material, int Quantity)> GetWarehouseContent(int warehouseId)
        {
            const string query = @"
                SELECT wc.material_id, wc.quantity, m.id, m.name, m.type, m.unit_price, m.vat_percent, m.specification
                FROM warehouse_content wc
                JOIN materials m ON m.id = wc.material_id
                WHERE wc.warehouse_id = @warehouseId;";

            var result = new List<(Material, int)>();

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@warehouseId", warehouseId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dto = new MaterialSerializationHelper.MaterialDTO
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    UnitPrice = reader.GetDouble("unit_price"),
                    VatPercent = reader.GetDouble("vat_percent")
                };

                dto.SetSpecification(reader.IsDBNull(reader.GetOrdinal("specification"))
                    ? "{}"
                    : reader.GetString("specification"));

                var material = MaterialSerializationHelper.DeserializeMaterial(dto);
                int quantity = reader.GetInt32("quantity");

                result.Add((material, quantity));
            }

            return result;
        }

        /// <summary>
        /// Adds a specified quantity of a material to a warehouse.
        /// If the material already exists, its quantity is increased.
        /// </summary>
        /// <param name="warehouseId">The target warehouse ID.</param>
        /// <param name="materialId">The ID of the material to add.</param>
        /// <param name="quantity">The amount of material to add.</param>
        /// <exception cref="ArgumentException">Thrown when quantity is less than or equal to zero.</exception>
        public void AddMaterialToWarehouse(int warehouseId, int materialId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");

            using var conn = _dbManager.GetOpenConnection();

            var checkCmd = new MySqlCommand(
                "SELECT quantity FROM warehouse_content WHERE warehouse_id=@wid AND material_id=@mid;", conn);
            checkCmd.Parameters.AddWithValue("@wid", warehouseId);
            checkCmd.Parameters.AddWithValue("@mid", materialId);

            object? existing = checkCmd.ExecuteScalar();

            if (existing == null)
            {
                var insertCmd = new MySqlCommand(@"
                    INSERT INTO warehouse_content (warehouse_id, material_id, quantity)
                    VALUES (@wid, @mid, @qty);", conn);
                insertCmd.Parameters.AddWithValue("@wid", warehouseId);
                insertCmd.Parameters.AddWithValue("@mid", materialId);
                insertCmd.Parameters.AddWithValue("@qty", quantity);
                insertCmd.ExecuteNonQuery();
            }
            else
            {
                var updateCmd = new MySqlCommand(@"
                    UPDATE warehouse_content 
                    SET quantity = quantity + @qty 
                    WHERE warehouse_id=@wid AND material_id=@mid;", conn);
                updateCmd.Parameters.AddWithValue("@wid", warehouseId);
                updateCmd.Parameters.AddWithValue("@mid", materialId);
                updateCmd.Parameters.AddWithValue("@qty", quantity);
                updateCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Removes a specified quantity of material from a warehouse.
        /// If the resulting quantity is zero or below, the material is removed entirely.
        /// </summary>
        /// <param name="warehouseId">The target warehouse ID.</param>
        /// <param name="materialId">The ID of the material to remove.</param>
        /// <param name="quantity">The amount to subtract.</param>
        /// <exception cref="ArgumentException">Thrown when quantity is less than or equal to zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the material is not found in the warehouse.</exception>
        public void RemoveMaterialFromWarehouse(int warehouseId, int materialId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");

            using var conn = _dbManager.GetOpenConnection();

            var checkCmd = new MySqlCommand(
                "SELECT quantity FROM warehouse_content WHERE warehouse_id=@wid AND material_id=@mid;", conn);
            checkCmd.Parameters.AddWithValue("@wid", warehouseId);
            checkCmd.Parameters.AddWithValue("@mid", materialId);

            object? result = checkCmd.ExecuteScalar();

            if (result == null)
                throw new InvalidOperationException("Material not found in warehouse.");

            int currentQty = Convert.ToInt32(result);
            int newQty = currentQty - quantity;

            if (newQty <= 0)
            {
                var delCmd = new MySqlCommand(
                    "DELETE FROM warehouse_content WHERE warehouse_id=@wid AND material_id=@mid;", conn);
                delCmd.Parameters.AddWithValue("@wid", warehouseId);
                delCmd.Parameters.AddWithValue("@mid", materialId);
                delCmd.ExecuteNonQuery();

                _materialRepo.Delete(materialId);
            }
            else
            {
                var updateCmd = new MySqlCommand(
                    "UPDATE warehouse_content SET quantity=@newQty WHERE warehouse_id=@wid AND material_id=@mid;", conn);
                updateCmd.Parameters.AddWithValue("@newQty", newQty);
                updateCmd.Parameters.AddWithValue("@wid", warehouseId);
                updateCmd.Parameters.AddWithValue("@mid", materialId);
                updateCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates the quantity of a specific material in a given warehouse.
        /// </summary>
        /// <param name="warehouseId">The target warehouse ID.</param>
        /// <param name="materialId">The ID of the material to update.</param>
        /// <param name="newQuantity">The new quantity value to set.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="newQuantity"/> is negative.</exception>
        public void UpdateMaterialQuantityInWarehouse(int warehouseId, int materialId, int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            using var conn = _dbManager.GetOpenConnection();

            var updateCmd = new MySqlCommand(@"
                UPDATE warehouse_content
                SET quantity = @qty
                WHERE warehouse_id = @wid AND material_id = @mid;", conn);
            updateCmd.Parameters.AddWithValue("@wid", warehouseId);
            updateCmd.Parameters.AddWithValue("@mid", materialId);
            updateCmd.Parameters.AddWithValue("@qty", newQuantity);
            updateCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Calculates the total monetary value of all materials stored in a specific warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to evaluate.</param>
        /// <returns>
        /// The total gross value of the warehouse inventory, calculated as
        /// <c>Σ (material.GrossPrice() × quantity)</c>.
        /// </returns>
        public double GetTotalWarehouseValue(int warehouseId)
        {
            double total = 0;

            var content = GetWarehouseContent(warehouseId);
            foreach (var (material, qty) in content)
            {
                total += material.GrossPrice() * qty;
            }

            return total;
        }
    }
}
