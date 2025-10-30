using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a styrofoam insulation material.
    /// Inherits from the <see cref="Material"/> base class and includes specific
    /// properties such as thickness, board size, and step resistance.
    /// </summary>
    public class Styrofoam : Material
    {
        /// <summary>
        /// Validator used to verify styrofoam-specific properties such as thickness.
        /// </summary>
        private readonly MaterialValidationService.StyrofoamValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.Styrofoam; }

        /// <summary>
        /// Enumerates the available board sizes for styrofoam insulation.
        /// </summary>
        public enum BoardSizes
        {
            /// <summary>
            /// Board size 50 × 50 cm.
            /// </summary>
            Size50x50,

            /// <summary>
            /// Board size 100 × 50 cm.
            /// </summary>
            Size100x50,

            /// <summary>
            /// Board size 100 × 100 cm.
            /// </summary>
            Size100x100
        }

        private double thickness;
        private bool stepResistant;
        private BoardSizes boardSize;

        /// <summary>
        /// Gets the validated thickness of the styrofoam in centimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the thickness is outside the valid range defined by
        /// <see cref="MaterialValidationService.StyrofoamValidator"/>.
        /// </exception>
        public double Thickness
        {
            get => thickness;
            private set => thickness = validator.ParseThickness(value);
        }

        /// <summary>
        /// Gets a value indicating whether the styrofoam is step-resistant.
        /// </summary>
        /// <remarks>
        /// Step-resistant styrofoam is designed to withstand mechanical loads, 
        /// making it suitable for use under floors or on flat roofs.
        /// </remarks>
        public bool StepResistant
        {
            get => stepResistant;
            private set => stepResistant = value;
        }

        /// <summary>
        /// Gets the size of the styrofoam board.
        /// </summary>
        public BoardSizes BoardSize
        {
            get => boardSize;
            private set => boardSize = value;
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public Styrofoam() : base(0, "Styrofoam", 0, 0, MaterialTypes.Insulation) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Styrofoam"/> class without a predefined ID.
        /// </summary>
        /// <param name="thickness">The thickness of the styrofoam in centimeters.</param>
        /// <param name="stepResistant">Indicates whether the styrofoam is step-resistant.</param>
        /// <param name="boardSize">The board size of the styrofoam.</param>
        /// <param name="price">The unit price of the styrofoam (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for thickness or price.</exception>
        public Styrofoam(double thickness, bool stepResistant, BoardSizes boardSize, double price, double vat)
            : base("Styrofoam", price, vat, MaterialTypes.Insulation)
        {
            Thickness = thickness;
            StepResistant = stepResistant;
            BoardSize = boardSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Styrofoam"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the styrofoam material.</param>
        /// <param name="thickness">The thickness of the styrofoam in centimeters.</param>
        /// <param name="stepResistant">Indicates whether the styrofoam is step-resistant.</param>
        /// <param name="boardSize">The board size of the styrofoam.</param>
        /// <param name="price">The unit price of the styrofoam (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for thickness or price.</exception>
        public Styrofoam(int id, double thickness, bool stepResistant, BoardSizes boardSize, double price, double vat)
            : base(id, "Styrofoam", price, vat, MaterialTypes.Insulation)
        {
            Thickness = thickness;
            StepResistant = stepResistant;
            BoardSize = boardSize;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { Thickness, StepResistant, BoardSize };

        /// <summary>
        /// Calculates the gross price of the styrofoam (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
