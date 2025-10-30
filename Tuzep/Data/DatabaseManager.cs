using DotNetEnv;
using MySqlConnector;
using System.Data;

namespace Tuzep.Data
{
    /// <summary>
    /// Provides database management utilities for initializing and managing the Túzép application's MySQL database.
    /// Handles connection setup, database creation, table creation, and data seeding.
    /// </summary>
    public class DatabaseManager
    {
        private readonly string _serverConnection;
        private readonly string _dbConnection;
        private readonly string _dbName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManager"/> class.
        /// Loads database configuration from environment variables and constructs
        /// connection strings for both server-level and database-level access.
        /// </summary>
        public DatabaseManager()
        {
            Env.Load();

            string host = Env.GetString("DB_HOST", "localhost");
            string port = Env.GetString("DB_PORT", "3306");
            _dbName = Env.GetString("DB_NAME", "tuzepkezelo");
            string user = Env.GetString("DB_USER", "root");
            string password = Env.GetString("DB_PASSWORD", "");
            string timeout = Env.GetString("DB_TIMEOUT", "30");

            _serverConnection = $"server={host};user={user};password={password};port={port};SslMode=none;Connection Timeout={timeout}";
            _dbConnection = $"{_serverConnection};database={_dbName}";
        }

        /// <summary>
        /// Initializes the database if it doesn't already exist.
        /// Creates the database, required tables, and seeds default warehouse data.
        /// </summary>
        public void InitializeDatabase()
        {
            CreateDatabase();
            CreateTables();
            SeedWarehouses();
        }

        /// <summary>
        /// Creates the database if it does not already exist.
        /// Character set and collation can be customized via environment variables.
        /// </summary>
        private void CreateDatabase()
        {
            string charset = Env.GetString("DB_CHARSET", "utf8mb4");
            string collation = Env.GetString("DB_COLLATION", "utf8mb4_general_ci");

            using var conn = new MySqlConnection(_serverConnection);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{_dbName}` CHARACTER SET {charset} COLLATE {collation};";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Creates the necessary tables for materials, warehouses, and warehouse contents.
        /// If the tables already exist, this operation has no effect.
        /// </summary>
        private void CreateTables()
        {
            using var conn = new MySqlConnection(_dbConnection);
            conn.Open();

            using var cmd = conn.CreateCommand();

            // MATERIALS TABLE
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS materials (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL,
                    type VARCHAR(50) NOT NULL,
                    unit_price DECIMAL(10,2) NOT NULL,
                    vat_percent DOUBLE NOT NULL,
                    specification JSON NULL
                );";
            cmd.ExecuteNonQuery();

            // WAREHOUSES TABLE
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS warehouses (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(50) NOT NULL
                );";
            cmd.ExecuteNonQuery();

            // WAREHOUSE_CONTENT TABLE
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS warehouse_content (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    warehouse_id INT NOT NULL,
                    material_id INT NOT NULL,
                    quantity INT NOT NULL DEFAULT 0,
                    FOREIGN KEY (warehouse_id) REFERENCES warehouses(id) ON DELETE CASCADE,
                    FOREIGN KEY (material_id) REFERENCES materials(id) ON DELETE CASCADE
                );";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Seeds initial warehouse entries into the database if none exist.
        /// Adds three default warehouses to simplify initial setup.
        /// </summary>
        private void SeedWarehouses()
        {
            using var conn = new MySqlConnection(_dbConnection);
            conn.Open();

            using var checkCmd = conn.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM warehouses;";
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count == 0)
            {
                using var insertCmd = conn.CreateCommand();
                insertCmd.CommandText = @"
                    INSERT INTO warehouses (name) VALUES
                    ('Warehouse 1'),
                    ('Warehouse 2'),
                    ('Warehouse 3');";
                insertCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a SQL query that returns a result set and fills it into a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A <see cref="DataTable"/> containing the query results.</returns>
        /// <exception cref="MySqlException">Thrown if the query fails or connection cannot be opened.</exception>
        public DataTable ExecuteQuery(string query)
        {
            using var conn = new MySqlConnection(_dbConnection);
            conn.Open();

            var adapter = new MySqlDataAdapter(query, conn);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        /// <summary>
        /// Executes a non-query SQL command (INSERT, UPDATE, DELETE, etc.) that does not return a result set.
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        /// <exception cref="MySqlException">Thrown if the query execution fails.</exception>
        public void ExecuteNonQuery(string query)
        {
            using var conn = new MySqlConnection(_dbConnection);
            conn.Open();

            using var cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a SQL query that returns a single scalar value (first column of the first row).
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        /// <returns>The scalar result as an <see cref="object"/>, or null if no result was found.</returns>
        /// <exception cref="MySqlException">Thrown if the query execution fails.</exception>
        public object ExecuteScalar(string query)
        {
            using var conn = new MySqlConnection(_dbConnection);
            conn.Open();

            using var cmd = new MySqlCommand(query, conn);
            return cmd.ExecuteScalar();
        }
    }
}
