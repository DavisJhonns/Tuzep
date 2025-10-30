using System.Reflection;
using Tuzep.Core.Model.MaterialModels;

namespace Tuzep.UI.Components
{
    /// <summary>
    /// Represents a user control for displaying and editing a single property of a <see cref="Material"/>.
    /// Automatically generates the correct input control based on the property type.
    /// </summary>
    public partial class MaterialPropertyUC : UserControl
    {
        /// <summary>
        /// Gets the property information associated with this control.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// The instance of <see cref="Material"/> whose property is displayed/edited.
        /// </summary>
        private readonly Material _materialInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialPropertyUC"/> class.
        /// </summary>
        /// <param name="materialProperty">The property to display/edit.</param>
        /// <param name="materialInstance">The material instance containing the property.</param>
        /// <param name="isEditable">Whether the property can be edited.</param>
        public MaterialPropertyUC(PropertyInfo materialProperty, Material materialInstance, bool isEditable)
        {
            Property = materialProperty;
            _materialInstance = materialInstance;
            InitializeComponent();
            LoadComponents(isEditable);
        }

        /// <summary>
        /// Loads and initializes the control(s) for the property, based on its type.
        /// </summary>
        /// <param name="isEditable">Whether the property can be edited.</param>
        private void LoadComponents(bool isEditable)
        {
            gbPropertyName.Text = $"{Property.Name}";
            gbPropertyName.Controls.Clear();

            var control = GenerateValueComponent();
            LoadCurrentValue(control);

            control.Enabled = isEditable;
            gbPropertyName.Controls.Add(control);
        }

        /// <summary>
        /// Generates the appropriate input control for the property type.
        /// </summary>
        /// <returns>A <see cref="Control"/> suitable for editing the property value.</returns>
        private Control GenerateValueComponent()
        {
            Type propertyType = Nullable.GetUnderlyingType(Property.PropertyType) ?? Property.PropertyType;

            return propertyType switch
            {
                Type t when t.IsEnum => GenerateEnumComponent(propertyType),
                Type t when t == typeof(string) => GenerateTextBoxComponent(),
                Type t when t == typeof(int) ||
                            t == typeof(decimal) ||
                            t == typeof(float) ||
                            t == typeof(double) => GenerateNumericUpDownComponent(),
                Type t when t == typeof(bool) => GenerateBoolComponent(),
                _ => new Label
                {
                    Text = $"Unsupported: {propertyType.Name}",
                    ForeColor = Color.Red,
                    Dock = DockStyle.Fill
                }
            };
        }

        /// <summary>
        /// Generates a ComboBox for enum properties.
        /// </summary>
        /// <param name="enumType">The enum type of the property.</param>
        /// <returns>A ComboBox populated with the enum values.</returns>
        private ComboBox GenerateEnumComponent(Type enumType)
        {
            ComboBox cmb = new()
            {
                Name = "cmbValue",
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            foreach (var value in Enum.GetValues(enumType))
                cmb.Items.Add(value);

            return cmb;
        }

        /// <summary>
        /// Generates a CheckBox for boolean properties.
        /// </summary>
        /// <returns>A CheckBox control.</returns>
        private CheckBox GenerateBoolComponent()
        {
            var chk = new CheckBox
            {
                Name = "chkValue",
                CheckAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            return chk;
        }

        /// <summary>
        /// Generates a TextBox for string properties.
        /// </summary>
        /// <returns>A TextBox control.</returns>
        private TextBox GenerateTextBoxComponent()
        {
            var txt = new TextBox
            {
                Name = "txtValue",
                TextAlign = HorizontalAlignment.Center,
                Dock = DockStyle.Fill
            };
            return txt;
        }

        /// <summary>
        /// Generates a NumericUpDown for numeric properties.
        /// </summary>
        /// <returns>A NumericUpDown control.</returns>
        private NumericUpDown GenerateNumericUpDownComponent()
        {
            var num = new NumericUpDown
            {
                Name = "numValue",
                TextAlign = HorizontalAlignment.Center,
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = decimal.MaxValue,
                DecimalPlaces = 1,
                Increment = 0.5M
            };
            return num;
        }

        /// <summary>
        /// Loads the current value of the property into the control.
        /// </summary>
        /// <param name="control">The control to populate with the value.</param>
        private void LoadCurrentValue(Control control)
        {
            if (_materialInstance == null || Property == null)
                return;

            object? currentValue = null;
            try
            {
                currentValue = Property.GetValue(_materialInstance);
            }
            catch { return; }

            switch (control)
            {
                case ComboBox cmb when currentValue != null:
                    cmb.SelectedItem = currentValue;
                    break;
                case CheckBox chk when currentValue != null:
                    chk.Checked = (bool)currentValue;
                    break;
                case TextBox txt:
                    txt.Text = currentValue?.ToString() ?? "";
                    break;
                case NumericUpDown num:
                    if (currentValue != null)
                    {
                        if (decimal.TryParse(currentValue.ToString(), out decimal val))
                            num.Value = Math.Clamp(val, num.Minimum, num.Maximum);
                    }
                    break;
            }
        }

        /// <summary>
        /// Applies the current value from the user control to the underlying property of the material instance.
        /// </summary>
        /// <returns>
        /// A <see cref="List{Exception}"/> containing any exceptions that occurred during the application of the value.
        /// If no exceptions occurred, the list will be empty.
        /// </returns>
        /// <remarks>
        /// This method automatically detects the type of the input control and converts the value accordingly:
        /// <list type="bullet">
        ///   <item><description>Enum properties are read from a <see cref="ComboBox"/>.</description></item>
        ///   <item><description>Boolean properties are read from a <see cref="CheckBox"/>.</description></item>
        ///   <item><description>String properties are read from a <see cref="TextBox"/>.</description></item>
        ///   <item><description>Numeric properties (int, decimal, float, double) are read from a <see cref="NumericUpDown"/>.</description></item>
        /// </list>
        /// If the property setter throws an exception (e.g., validation failure), it is caught and added to the returned list.  
        /// Exceptions thrown by unsupported control types are also captured.  
        /// <para><code><b>Note:</b> When running under a debugger, some exceptions may cause the debugger
        ///      to break even though they are caught by this method.<br/>
        ///      This is normal behavior and does not indicate an unhandled exception.
        /// </code></para>
        /// </remarks>
        public List<Exception> ApplyChanges()
        {
            var errors = new List<Exception>();

            if (gbPropertyName.Controls.Count == 0)
                return errors;

            Control control = gbPropertyName.Controls[0];
            object? newValue = null;
            Type targetType = Nullable.GetUnderlyingType(Property.PropertyType) ?? Property.PropertyType;

            try
            {
                switch (control)
                {
                    case ComboBox cmb when targetType.IsEnum:
                        newValue = Enum.Parse(targetType, cmb.SelectedItem?.ToString() ?? "");
                        break;

                    case CheckBox chk when targetType == typeof(bool):
                        newValue = chk.Checked;
                        break;

                    case TextBox txt:
                        newValue = txt.Text;
                        break;

                    case NumericUpDown num:
                        newValue = Convert.ChangeType(num.Value, targetType);
                        break;

                    default:
                        throw new InvalidOperationException("Unsupported control type.");
                }

                Property.SetValue(_materialInstance, newValue);
            }
            catch (TargetInvocationException tie)
            {
                errors.Add(tie.InnerException ?? tie);
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }

            return errors;
        }
    }
}
