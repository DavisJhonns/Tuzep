using Tuzep.Data;
using Tuzep.Services;
using Tuzep.UI.Forms;

namespace Tuzep.UI
{
    public partial class MainForm : Form
    {
        private readonly DatabaseManager _db;
        private readonly WarehouseService _warehouseService;
        public MainForm(WarehouseService ws)
        {
            InitializeComponent();
            _warehouseService = ws;
        }

        private void btnWarehouses_Click(object sender, EventArgs e)
        {
            var warehouseForm = new WarehouseForm(_warehouseService);
            warehouseForm.ShowDialog();
        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            _warehouseService.ImportMaterialFromCsv();
        }
    }
}
