using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a brick material with defined form and thickness.
    /// Inherits from the <see cref="Material"/> base class and adds brick-specific validation.
    /// </summary>
    public class Brick : Material
    {
        /// <summary>
        /// Validator used to verify brick-specific properties such as thickness.
        /// </summary>
        private readonly MaterialValidationService.BrickValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon => Resources.MaterialIcons.Brick;

        /// <summary>
        /// Enumerates possible brick forms.
        /// </summary>
        public enum Forms
        {
            /// <summary>
            /// A solid brick without internal air cells.
            /// </summary>
            Solid,

            /// <summary>
            /// A brick with air cells (hollow brick).
            /// </summary>
            AirCell
        }

        private Forms form;
        private double thickness;

        /// <summary>
        /// Gets the form of the brick (solid or air-cell).
        /// </summary>
        public Forms Form
        {
            get => form;
            private set => form = value;
        }

        /// <summary>
        /// Gets the validated thickness of the brick in centimeters.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the thickness is outside the allowed range defined by <see cref="MaterialValidationService.BrickValidator"/>.
        /// </exception>
        public double Thickness
        {
            get => thickness;
            private set => thickness = validator.ParseThickness(value);
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public Brick() : base(0, nameof(Brick), 1, 1, MaterialTypes.Hard) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class without a predefined ID.
        /// </summary>
        /// <param name="form">The form of the brick (solid or air-cell).</param>
        /// <param name="thickness">The thickness of the brick in centimeters.</param>
        /// <param name="price">The unit price of the brick (net price).</param>
        /// <param name="vat">The VAT percentage applied to the brick.</param>
        /// <exception cref="ArgumentException">Thrown when any validation fails.</exception>
        public Brick(Forms form, double thickness, double price, double vat)
            : base(0, nameof(Brick), price, vat, MaterialTypes.Hard)
        {
            Form = form;
            Thickness = thickness;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the brick.</param>
        /// <param name="form">The form of the brick (solid or air-cell).</param>
        /// <param name="thickness">The thickness of the brick in centimeters.</param>
        /// <param name="price">The unit price of the brick (net price).</param>
        /// <param name="vat">The VAT percentage applied to the brick.</param>
        /// <exception cref="ArgumentException">Thrown when any validation fails.</exception>
        public Brick(int id, Forms form, double thickness, double price, double vat)
            : base(id, nameof(Brick), price, vat, MaterialTypes.Hard)
        {
            Form = form;
            Thickness = thickness;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { Form, Thickness };

        /// <summary>
        /// Calculates the gross price of the brick (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
