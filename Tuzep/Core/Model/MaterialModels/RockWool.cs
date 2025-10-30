using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a rock wool insulation material.
    /// Inherits from the <see cref="Material"/> base class and includes specific
    /// properties such as thickness and form (rolled or board).
    /// </summary>
    public class RockWool : Material
    {
        /// <summary>
        /// Validator used to verify rock wool–specific properties such as thickness.
        /// </summary>
        private readonly MaterialValidationService.RockWoolValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.RockWool; }

        /// <summary>
        /// Enumerates the available forms of rock wool insulation.
        /// </summary>
        public enum Forms
        {
            /// <summary>
            /// Rolled rock wool insulation, typically used for attics or large surface coverage.
            /// </summary>
            Rolled,

            /// <summary>
            /// Board-shaped rock wool insulation, commonly used for walls, facades, or floors.
            /// </summary>
            Board
        }

        private double thickness;
        private Forms form;

        /// <summary>
        /// Gets the validated thickness of the rock wool in centimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the thickness is outside the allowed range defined by
        /// <see cref="MaterialValidationService.RockWoolValidator"/>.
        /// </exception>
        public double Thickness
        {
            get => thickness;
            private set => thickness = validator.ParseThickness(value);
        }

        /// <summary>
        /// Gets the form of the rock wool (rolled or board).
        /// </summary>
        public Forms Form
        {
            get => form;
            private set => form = value;
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public RockWool() : base(0, "RockWool", 0, 0, MaterialTypes.Insulation) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RockWool"/> class without a predefined ID.
        /// </summary>
        /// <param name="thickness">The thickness of the rock wool in centimeters.</param>
        /// <param name="form">The form of the rock wool (rolled or board).</param>
        /// <param name="price">The unit price of the rock wool (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for thickness or price.</exception>
        public RockWool(double thickness, Forms form, double price, double vat)
            : base("RockWool", price, vat, MaterialTypes.Insulation)
        {
            Thickness = thickness;
            Form = form;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RockWool"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the rock wool material.</param>
        /// <param name="thickness">The thickness of the rock wool in centimeters.</param>
        /// <param name="form">The form of the rock wool (rolled or board).</param>
        /// <param name="price">The unit price of the rock wool (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for thickness or price.</exception>
        public RockWool(int id, double thickness, Forms form, double price, double vat)
            : base(id, "RockWool", price, vat, MaterialTypes.Insulation)
        {
            Thickness = thickness;
            Form = form;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { Thickness, Form };

        /// <summary>
        /// Calculates the gross price of the rock wool (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
