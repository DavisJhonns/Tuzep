using System.Data;
using System.Reflection;
using Tuzep.Core.Model.MaterialModels;
using Tuzep.Services;
using Tuzep.UI.Components;

namespace Tuzep.UI.Forms
{
    /// <summary>
    /// Represents a form that displays detailed information about a <see cref="Material"/>.
    /// The form supports three modes:
    /// - **View Mode**: Displays material properties in read-only form.
    /// - **Edit Mode**: Allows updating the material’s quantity.
    /// - **Create Mode**: Allows creating a new material instance.
    /// </summary>
    public partial class MaterialDetailForm : Form
    {
        /// <summary>
        /// The material currently being viewed, created, or edited.
        /// </summary>
        public Material CurrentMaterial { get; private set; }

        /// <summary>
        /// Gets the updated quantity value (used when in edit mode).
        /// </summary>
        public int UpdatedQuantity => _quantity;

        private readonly bool _createMode;
        private readonly bool _editMode;
        private int _quantity;

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the form in **view mode**, showing an existing material as read-only.
        /// </summary>
        /// <param name="material">The material to display.</param>
        public MaterialDetailForm(Material material)
        {
            InitializeComponent();
            _createMode = false;
            _editMode = false;
            CurrentMaterial = material ?? throw new ArgumentNullException(nameof(material));
        }

        /// <summary>
        /// Initializes the form in **edit mode**, allowing the user to modify the material's quantity.
        /// </summary>
        /// <param name="material">The material to edit.</param>
        /// <param name="quantity">The current quantity of the material in the warehouse.</param>
        public MaterialDetailForm(Material material, int quantity)
        {
            InitializeComponent();
            _createMode = false;
            _editMode = true;
            _quantity = quantity;
            CurrentMaterial = material ?? throw new ArgumentNullException(nameof(material));
        }

        /// <summary>
        /// Initializes the form in **create mode**, allowing the user to create a new material instance.
        /// </summary>
        /// <param name="materialType">The type of material to create.</param>
        public MaterialDetailForm(Type materialType)
        {
            InitializeComponent();
            _createMode = true;
            _editMode = false;
            CurrentMaterial = CreateMaterialInstance(materialType);
        }

        #endregion

        #region PROPERTY CONTROLS

        /// <summary>
        /// Dynamically generates UI components for each property of the <see cref="Material"/> object.
        /// </summary>
        private void GenerateComponentsFromProperties()
        {
            flpProperties.Controls.Clear();

            var editableMap = GetEditableMap();
            var properties = GetOrderedProperties(CurrentMaterial, editableMap, nameof(CurrentMaterial.Name), nameof(CurrentMaterial.Icon));

            foreach (var prop in properties)
            {
                // Determine editability
                bool isEditable = editableMap.TryGetValue(prop.Name, out bool editable) ? editable : true;

                // Disable editing in view mode
                if (!_createMode && !_editMode)
                    isEditable = false;

                var propertyControl = new MaterialPropertyUC(prop, CurrentMaterial, isEditable)
                {
                    Dock = DockStyle.Top
                };

                flpProperties.Controls.Add(propertyControl);
            }

            SetLayoutSizes(flpProperties);
        }

        /// <summary>
        /// Adds or displays a quantity control depending on the mode (create, edit, or view).
        /// </summary>
        private void InitializeQuantityControl()
        {
            if (_createMode || _editMode)
            {
                // Editable quantity field
                var quantityControl = new NumericUpDown
                {
                    Name = "nudQuantity",
                    Minimum = 1,
                    Maximum = 1_000_000,
                    Value = _quantity,
                    Dock = DockStyle.Top
                };

                quantityControl.ValueChanged += (s, e) => _quantity = (int)quantityControl.Value;

                flpProperties.Controls.Add(new Label { Text = "Quantity", Dock = DockStyle.Top });
                flpProperties.Controls.Add(quantityControl);

                flpProperties.Height += quantityControl.Height + 20;
                this.Height += quantityControl.Height + 20;
            }
            else
            {
                // Read-only display
                var quantityLabel = new Label
                {
                    Text = $"Quantity: {_quantity}",
                    Dock = DockStyle.Top
                };
                flpProperties.Controls.Add(quantityLabel);
            }
        }

        #endregion

        #region HELPERS

        /// <summary>
        /// Adjusts layout sizes dynamically to ensure a clean and responsive property grid.
        /// </summary>
        private void SetLayoutSizes(Control container)
        {
            const int COLS = 2;
            const int VERTICAL_INDENT = 6;
            const int HORIZONTAL_INDENT = 6;
            const int VERTICAL_SPACING = 25;

            var controls = container.Controls.OfType<MaterialPropertyUC>().ToList();
            if (!controls.Any()) return;

            int rows = (int)Math.Ceiling(controls.Count / (double)COLS);
            int itemHeight = controls.First().Height;
            int top = container.Location.Y;

            foreach (var item in controls)
                item.Width = container.ClientSize.Width / COLS - HORIZONTAL_INDENT;

            container.Height = rows * (itemHeight + VERTICAL_INDENT) + VERTICAL_INDENT;
            this.Height = VERTICAL_SPACING + top + container.Height + btnOk.Height + VERTICAL_SPACING;
        }

        /// <summary>
        /// Defines which material properties are editable in the UI.
        /// </summary>
        private Dictionary<string, bool> GetEditableMap() => new()
        {
            { nameof(CurrentMaterial.Id), false },
            { nameof(CurrentMaterial.MaterialType), false },
            { nameof(CurrentMaterial.VatPercent), true },
            { nameof(CurrentMaterial.UnitPrice), true }
        };

        /// <summary>
        /// Returns an ordered list of material properties for UI generation.
        /// </summary>
        private List<PropertyInfo> GetOrderedProperties(object obj, Dictionary<string, bool> order, params string[] exclude)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                .Where(p => p.CanRead && !exclude.Contains(p.Name))
                .OrderBy(p =>
                {
                    int index = order.Keys.ToList().IndexOf(p.Name);
                    return index >= 0 ? index : int.MaxValue;
                })
                .ThenBy(p => p.Name)
                .ToList();
        }

        /// <summary>
        /// Creates a new instance of the given material type using its available constructor.
        /// </summary>
        private Material CreateMaterialInstance(Type materialType)
        {
            if (materialType == null || !typeof(Material).IsAssignableFrom(materialType))
                throw new ArgumentException("Invalid material type", nameof(materialType));

            var ctor = materialType.GetConstructor(Type.EmptyTypes);
            if (ctor != null)
                return (Material)Activator.CreateInstance(materialType)!;

            ctor = materialType.GetConstructor(new[] { typeof(bool) });
            if (ctor != null)
                return (Material)ctor.Invoke(new object[] { true });

            return (Material)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(materialType);
        }

        #endregion

        #region EVENTS

        private void MaterialDetailForm_Load(object sender, EventArgs e)
        {
            lblMaterialName.Text = CurrentMaterial.Name;
            picExportIcon.Image = CurrentMaterial.Icon;

            GenerateComponentsFromProperties();

            if (_editMode)
                InitializeQuantityControl();
        }

        /// <summary>
        /// Handles the OK button click — validates input and applies all property changes.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            var propertyControls = flpProperties.Controls.OfType<MaterialPropertyUC>();
            var allErrors = propertyControls.SelectMany(c => c.ApplyChanges()).ToList();

            if (allErrors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, allErrors.Select(ex => ex.Message)),
                    "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Closes the form without saving changes.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Exports the current material to a CSV file when the export icon is clicked.
        /// Only available in view mode.
        /// </summary>
        private void picExportIcon_Click(object sender, EventArgs e)
        {
            if (!_createMode && !_editMode)
                CsvService.ExportCSV(CurrentMaterial);
        }

        #endregion
    }
}
