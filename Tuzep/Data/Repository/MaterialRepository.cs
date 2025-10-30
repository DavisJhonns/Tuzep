using MySql.Data.MySqlClient;
using Tuzep.Core.Model.MaterialModels;
using Tuzep.Data.Helpers;

namespace Tuzep.Data.Repository
{
    /// <summary>
    /// Repository class responsible for managing all database operations
    /// related to <see cref="Material"/> entities, including creation, retrieval,
    /// updating, and deletion.
    /// </summary>
    /// <remarks>
    /// This repository interacts directly with the MySQL database through the <see cref="DatabaseManager"/>
    /// and relies on <see cref="MaterialSerializationHelper"/> for serializing and deserializing
    /// material specifications stored as JSON.
    /// </remarks>
    public class MaterialRepository
    {
        private readonly DatabaseManager _dbManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialRepository"/> class.
        /// </summary>
        /// <param name="dbManager">
        /// The database manager used to establish and manage MySQL connections.
        /// </param>
        public MaterialRepository(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        /// <summary>
        /// Inserts a new material record into the <c>materials</c> table.
        /// </summary>
        /// <param name="material">The <see cref="Material"/> instance to insert.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided material is null.</exception>
        public void Insert(Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            var dto = MaterialSerializationHelper.SerializeMaterial(material);
            string jsonSpec = dto.GetSpecificationAsJson();

            const string query = @"
                INSERT INTO materials (name, type, unit_price, vat_percent, specification)
                VALUES (@name, @type, @unit_price, @vat_percent, @spec);";

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@name", material.Name);
            cmd.Parameters.AddWithValue("@type", material.MaterialType.ToString());
            cmd.Parameters.AddWithValue("@unit_price", material.UnitPrice);
            cmd.Parameters.AddWithValue("@vat_percent", material.VatPercent);
            cmd.Parameters.AddWithValue("@spec", jsonSpec);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Retrieves a material from the database that matches the given material’s full specification.
        /// </summary>
        /// <param name="material">
        /// The <see cref="Material"/> whose specification will be compared to existing entries.
        /// </param>
        /// <returns>
        /// A matching <see cref="Material"/> instance if found; otherwise, <c>null</c>.
        /// </returns>
        public Material? GetBySpecification(Material material)
        {
            var incomeDto = MaterialSerializationHelper.SerializeMaterial(material);
            string jsonSpec = incomeDto.GetSpecificationAsJson();

            const string query = "SELECT * FROM materials WHERE specification = @spec;";

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@spec", jsonSpec);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var dto = new MaterialSerializationHelper.MaterialDTO
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    UnitPrice = reader.GetDouble("unit_price"),
                    VatPercent = reader.GetDouble("vat_percent")
                };
                dto.SetSpecification(reader.IsDBNull(reader.GetOrdinal("specification")) ? "{}" : reader.GetString("specification"));

                return MaterialSerializationHelper.DeserializeMaterial(dto);
            }

            return null;
        }

        /// <summary>
        /// Updates the price, VAT, and specification of an existing material in the database.
        /// </summary>
        /// <param name="material">The material entity with updated data.</param>
        public void Update(Material material)
        {
            var dto = MaterialSerializationHelper.SerializeMaterial(material);
            string jsonSpec = dto.GetSpecificationAsJson();

            const string query = @"
                UPDATE materials 
                SET unit_price = @unit_price, 
                    vat_percent = @vat_percent, 
                    specification = @spec
                WHERE id = @id;";

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@id", material.Id);
            cmd.Parameters.AddWithValue("@unit_price", material.UnitPrice);
            cmd.Parameters.AddWithValue("@vat_percent", material.VatPercent);
            cmd.Parameters.AddWithValue("@spec", jsonSpec);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permanently removes a material from the <c>materials</c> table by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the material to delete.</param>
        public void Delete(int id)
        {
            const string query = "DELETE FROM materials WHERE id = @id;";

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Retrieves all materials stored in the database.
        /// </summary>
        /// <returns>A list of all <see cref="Material"/> instances.</returns>
        public List<Material> GetAll()
        {
            const string query = "SELECT * FROM materials;";
            var result = new List<Material>();

            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
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
                dto.SetSpecification(reader.IsDBNull(reader.GetOrdinal("specification")) ? "{}" : reader.GetString("specification"));

                var material = MaterialSerializationHelper.DeserializeMaterial(dto);
                result.Add(material);
            }

            return result;
        }

        /// <summary>
        /// Retrieves a material by its unique database identifier.
        /// </summary>
        /// <param name="id">The material ID to look up.</param>
        /// <returns>
        /// The corresponding <see cref="Material"/> instance if found; otherwise, <c>null</c>.
        /// </returns>
        public Material? GetById(int id)
        {
            const string query = "SELECT * FROM materials WHERE id = @id;";
            using var conn = _dbManager.GetOpenConnection();
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var dto = new MaterialSerializationHelper.MaterialDTO
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    UnitPrice = reader.GetDouble("unit_price"),
                    VatPercent = reader.GetDouble("vat_percent")
                };
                dto.SetSpecification(reader.IsDBNull(reader.GetOrdinal("specification")) ? "{}" : reader.GetString("specification"));

                return MaterialSerializationHelper.DeserializeMaterial(dto);
            }
            return null;
        }
    }
    #region EXTENSION

    /// <summary>
    /// Provides utility extensions for the <see cref="DatabaseManager"/> class,
    /// allowing easy access to open MySQL connections.
    /// </summary>
    public static class DatabaseManagerExtensions
    {
        /// <summary>
        /// Opens and returns a new <see cref="MySqlConnection"/> using the
        /// connection string from the internal <see cref="DatabaseManager"/> instance.
        /// </summary>
        /// <param name="db">The database manager instance providing connection details.</param>
        /// <returns>An open <see cref="MySqlConnection"/> ready for use.</returns>
        public static MySqlConnection GetOpenConnection(this DatabaseManager db)
        {
            var field = typeof(DatabaseManager).GetField("_dbConnection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            string connStr = (string)field?.GetValue(db)!;
            var conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
    }

    #endregion
}
