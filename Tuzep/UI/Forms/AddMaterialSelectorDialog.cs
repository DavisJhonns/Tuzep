using Tuzep.Core.Model.MaterialModels;
using static Tuzep.Data.Repository.WarehouseRepository;

namespace Tuzep.UI.Forms
{
    /// <summary>
    /// Dialog for selecting a material, the quantity, and the warehouse it belongs to.
    /// </summary>
    public partial class AddMaterialSelectorDialog : Form
    {
        /// <summary>
        /// Data Transfer Object containing the selected material, quantity, and warehouse.
        /// </summary>
        public struct SpecifiedMaterialDTO
        {
            /// <summary>
            /// ID of the selected warehouse.
            /// </summary>
            public int WarehouseId { get; set; }

            /// <summary>
            /// Type of the selected material (e.g., Brick, Ytong).
            /// </summary>
            public Type MaterialType { get; set; }

            /// <summary>
            /// Quantity of the material.
            /// </summary>
            public int Quantity { get; set; }
        }

        /// <summary>
        /// List of warehouses available for selection.
        /// </summary>
        private readonly List<WarehouseDTO> _availableWarehouses;

        /// <summary>
        /// The material selected by the user. Populated when OK is clicked.
        /// </summary>
        public SpecifiedMaterialDTO SelectedMaterial { get; private set; }

        /// <summary>
        /// Constructor. Initializes the dialog with a list of available warehouses.
        /// </summary>
        /// <param name="availableWarehouses">The list of warehouses the user can select from.</param>
        public AddMaterialSelectorDialog(List<WarehouseDTO> availableWarehouses)
        {
            InitializeComponent();
            _availableWarehouses = availableWarehouses;
        }

        /// <summary>
        /// Initializes the UI controls when the form loads.
        /// Populates the material types and warehouse dropdowns.
        /// </summary>
        private void AddMaterialSelectorDialog_Load(object sender, EventArgs e)
        {
            cmbSpecifiedMaterial.DataSource = Enum.GetValues(typeof(AvaibleMaterials.MaterialTypes));
            cmbSpecifiedMaterial.SelectedIndex = -1;

            cmbWarehouse.DataSource = _availableWarehouses;
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Id";
            cmbWarehouse.SelectedIndex = -1;

            nudQuantity.Minimum = 1;
            nudQuantity.Value = 1;
        }

        /// <summary>
        /// Handles the OK button click event.
        /// Validates input and sets SelectedMaterial if all inputs are valid.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            var errors = new List<string>();

            WarehouseDTO? warehouse = cmbWarehouse.SelectedItem is WarehouseDTO w ? w : (WarehouseDTO?)null;
            if (warehouse == null)
                errors.Add("Please select a warehouse.");

            var materialEnum = cmbSpecifiedMaterial.SelectedItem as AvaibleMaterials.MaterialTypes?;
            Type materialType = null;
            if (materialEnum.HasValue)
                materialType = AvaibleMaterials.MaterialTypeMap[materialEnum.Value];
            else
                errors.Add("Please select a material type.");

            int quantity = (int)nudQuantity.Value;
            if (quantity <= 0)
                errors.Add("Quantity must be at least 1.");

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedMaterial = new SpecifiedMaterialDTO
            {
                WarehouseId = warehouse.Value.Id,
                MaterialType = materialType,
                Quantity = quantity
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// Closes the dialog without saving any selections.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
