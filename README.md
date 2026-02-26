# Tuzep

**Tuzep** is a C# **Windows Forms Application (WFA)** that connects to a MySQL database for data management.  
The application **requires a valid database connection** — if the connection fails, the program will not start.

---

# Dependencies (NuGet Packages)

```bash
  DotNetEnv 3.1.1
  MySqlConnector 2.4.0
```

# Environment Configuration (.env file)

The application uses a `.env` file to load MySQL connection settings.  
If the `.env` file is missing or corrupted, the default configuration below will be loaded:

```python
# MySQL Database Configuration
DB_HOST=localhost
DB_PORT=3306
DB_NAME=tuzepkezelo
DB_USER=root
DB_PASSWORD=

# Optional: Connection settings
DB_SSLMODE=none
DB_TIMEOUT=30

# Optional: Character set and collation
DB_CHARSET=utf8mb4
DB_COLLATION=utf8mb4_general_ci
```

# Setup Process

1. **Clone the repository**

   ```bash
   git clone https://github.com/DavisJhonns/Tuzep.git
   cd Tuzep
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Build the solution**

   ```bash
   dotnet build Tuzep.sln
   ```

---

## Running the Application

You can start the application in one of the following ways:

### 1. Using Visual Studio

1. Open `Tuzep.sln` in **Visual Studio**.
2. Ensure the startup project is set to the main Windows Forms project.
3. Press **F5** or click the **Start** button.

### 2. From the Command Line

1. Navigate to the build output directory:

   ```bash
   cd Tuzep\bin\Debug\net8.0-windows
   ```
2. Run the executable:

   ```bash
   ./Tuzep.exe
   ```

---

# Notes

* The program will not launch without a working MySQL connection.
* Ensure **MySQL Server** is running and credentials in the `.env` file are correct.
* You can modify connection settings directly in the `.env` file without recompiling the app.
