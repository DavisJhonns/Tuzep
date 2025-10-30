using Tuzep.Core.Model.MaterialModels;
using Tuzep.Data;
using Tuzep.Data.Repository;
using Tuzep.Services;
using Tuzep.UI;

namespace Tuzep
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                var db = new DatabaseManager();
                db.InitializeDatabase();

                var materialRepo = new MaterialRepository(db);
                var warehouseRepo = new WarehouseRepository(db, materialRepo);
                var service = new WarehouseService(materialRepo, warehouseRepo);

                Application.Run(new MainForm(service));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal error: {ex.Message}", "Application Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}