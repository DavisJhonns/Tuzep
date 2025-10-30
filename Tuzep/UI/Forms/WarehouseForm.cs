using System.Data;
using Tuzep.Core.Model.MaterialModels;
using Tuzep.Services;

namespace Tuzep.UI.Forms
{
    /// <summary>
    /// Represents the form for managing warehouses and their material contents.
    /// </summary>
    public partial class WarehouseForm : Form
    {
        private readonly WarehouseService _service;
        private int _selectedWarehouseId;
        private List<Material>? _materials;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseForm"/> class.
        /// </summary>
        /// <param name="service">The warehouse service for data access.</param>
        public WarehouseForm(WarehouseService service)
        {
            InitializeComponent();
            _service = service;
        }

        /// <summary>
        /// Handles form load event, initializes warehouse and material type data.
        /// </summary>
        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
            cmbMaterialType.Items.AddRange(Enum.GetNames(typeof(MaterialTypes)));
            ResetFilterFields();
        }

        #region Warehouse Loading

        /// <summary>
        /// Loads all warehouses into the combo box.
        /// </summary>
        private void LoadWarehouses()
        {
            var warehouses = _service.GetAllWarehouses();

            cmbWarehouses.DataSource = warehouses;
            cmbWarehouses.DisplayMember = "Name";
            cmbWarehouses.ValueMember = "Id";

            if (warehouses.Any())
            {
                _selectedWarehouseId = warehouses.First().Id;
                LoadWarehouseContent(_selectedWarehouseId);
            }
        }

        /// <summary>
        /// Loads the content of a selected warehouse into the DataGridView.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to load.</param>
        private void LoadWarehouseContent(int warehouseId)
        {
            List<(Material, int)> content = _service.GetWarehouseContent(warehouseId);
            _materials = content.Select(c => c.Item1).ToList();

            dgvWarehouseContent.Rows.Clear();

            foreach (var (mat, qty) in content)
            {
                dgvWarehouseContent.Rows.Add(
                    mat.Icon,
                    mat.Id,
                    mat.Name,
                    mat.MaterialType,
                    mat.UnitPrice,
                    mat.VatPercent,
                    mat.GrossPrice(),
                    qty,
                    mat.GetUniqueProperties().ToString()
                );
            }

            double totalValue = _service.GetTotalWarehouseValue(warehouseId);
            lblTotalValue.Text = $"Total value: {totalValue:C}";
        }

        /// <summary>
        /// Handles warehouse selection change.
        /// </summary>
        private void cmbWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWarehouses.SelectedValue is int id)
            {
                _selectedWarehouseId = id;
                LoadWarehouseContent(id);
            }
        }

        #endregion

        #region Material Management

        /// <summary>
        /// Adds a new material to the selected warehouse.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialog = new AddMaterialSelectorDialog(_service.GetAllWarehouses());
            dialog.ShowDialog();

            if (dialog.DialogResult == DialogResult.OK)
            {
                var materialInfo = dialog.SelectedMaterial;
                var matDialog = new MaterialDetailForm(dialog.SelectedMaterial.MaterialType);

                if (matDialog.ShowDialog() == DialogResult.OK)
                {
                    _service.AddOrUpdateMaterialInWarehouse(matDialog.CurrentMaterial, materialInfo.WarehouseId, materialInfo.Quantity);
                    LoadWarehouseContent(_selectedWarehouseId);
                }
            }
        }

        /// <summary>
        /// Edits the selected material in the warehouse.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvWarehouseContent.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select a single material to edit.");
                return;
            }

            int materialId = Convert.ToInt32(dgvWarehouseContent.SelectedRows[0].Cells["Id_col"].Value);
            int quantity = Convert.ToInt32(dgvWarehouseContent.SelectedRows[0].Cells["Quantity_col"].Value);
            var material = _materials?.FirstOrDefault(m => m.Id == materialId);

            if (material == null)
            {
                MessageBox.Show("Selected material not found.");
                return;
            }

            var dialog = new MaterialDetailForm(material, quantity);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _service.UpdateMaterial(dialog.CurrentMaterial);
                    _service.UpdateQuantityInWarehouse(material.Id, _selectedWarehouseId, dialog.UpdatedQuantity);
                    LoadWarehouseContent(_selectedWarehouseId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while updating: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Removes selected materials from the warehouse.
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var dialog = new RemoveMaterialDialog();
            if (dialog.ShowDialog() == DialogResult.Cancel) return;

            try
            {
                var selectedMaterialIds = dgvWarehouseContent.SelectedRows.Cast<DataGridViewRow>()
                    .Select(row => Convert.ToInt32(row.Cells["Id_col"].Value)).ToList();

                foreach (var materialId in selectedMaterialIds)
                {
                    _service.RemoveMaterial(_selectedWarehouseId, materialId, dialog.QuantityToDelete);
                }

                LoadWarehouseContent(_selectedWarehouseId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while removing: {ex.Message}");
            }
        }

        #endregion

        #region Refresh & Value

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadWarehouseContent(_selectedWarehouseId);
        }

        private void btnValue_Click(object sender, EventArgs e)
        {
            try
            {
                double total = _service.GetTotalWarehouseValue(_selectedWarehouseId);
                MessageBox.Show($"Total value: {total:C}");
                lblTotalValue.Text = $"Total value: {total:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating value: {ex.Message}");
            }
        }

        #endregion

        #region Filtering

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string name = txtFilterName.Text.Trim();
            double min = (double)nudMinPrice.Value;
            double max = (double)nudMaxPrice.Value;

            MaterialTypes? type = cmbMaterialType.SelectedItem is string typeStr &&
                                  Enum.TryParse(typeStr, out MaterialTypes parsed)
                                  ? parsed
                                  : null;

            List<(Material, int)> content = _service.GetWarehouseContent(_selectedWarehouseId);
            _materials = content.Select(c => c.Item1).ToList();

            var filtered = _service.FilterMaterials(_materials, name, min, max, type);

            var reduced = content.Where(c => filtered.Any(f => f.Id == c.Item1.Id)).ToList();

            dgvWarehouseContent.Rows.Clear();

            foreach (var (mat, qty) in reduced)
            {
                dgvWarehouseContent.Rows.Add(
                    mat.Icon,
                    mat.Id,
                    mat.Name,
                    mat.MaterialType,
                    mat.UnitPrice,
                    mat.VatPercent,
                    mat.GrossPrice(),
                    qty,
                    mat.GetUniqueProperties().ToString()
                );
            }

            lblTotalValue.Text = $"Filtered materials: {filtered.Count}";
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            ResetFilterFields();
            LoadWarehouseContent(_selectedWarehouseId);
        }

        /// <summary>
        /// Resets all filter fields to default values.
        /// </summary>
        private void ResetFilterFields()
        {
            txtFilterName.Text = "";
            cmbMaterialType.SelectedIndex = -1;
            nudMinPrice.Value = 0;

            if (_materials == null || !_materials.Any())
            {
                nudMaxPrice.Value = 0;
                return;
            }

            nudMaxPrice.Maximum = (decimal)_materials.Max(m => m.UnitPrice) + 1;
            nudMaxPrice.Value = (decimal)_materials.Max(m => m.UnitPrice);
        }

        #endregion

        #region DataGridView Events

        private void dgvWarehouseContent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _materials == null) return;

            int id = Convert.ToInt32(dgvWarehouseContent.Rows[e.RowIndex].Cells["Id_col"].Value);
            var mat = _materials.FirstOrDefault(m => m.Id == id);

            if (mat != null)
            {
                var dialog = new MaterialDetailForm(mat);
                dialog.ShowDialog();
            }
        }

        #endregion
    }
}
