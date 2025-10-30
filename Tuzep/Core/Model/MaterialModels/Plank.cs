using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a wooden plank material.
    /// Inherits from the <see cref="Material"/> base class and includes specific
    /// properties such as size, length, and insect treatment status.
    /// </summary>
    public partial class Plank : Material
    {
        /// <summary>
        /// Validator used to verify plank-specific properties such as length.
        /// </summary>
        private readonly MaterialValidationService.PlankValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.Plank; }

        /// <summary>
        /// Enumerates the available standard sizes of planks.
        /// </summary>
        public enum Sizes
        {
            /// <summary>
            /// Plank with a cross-section of 5 × 10 cm.
            /// </summary>
            Size_5x10_cm,

            /// <summary>
            /// Plank with a cross-section of 5 × 15 cm.
            /// </summary>
            Size_5x15_cm,

            /// <summary>
            /// Plank with a cross-section of 7.5 × 15 cm.
            /// </summary>
            Size_7_5x15_cm,

            /// <summary>
            /// Plank with a cross-section of 7.5 × 20 cm.
            /// </summary>
            Size_7_5x20_cm
        }

        private Sizes size;
        private double length;
        private bool insectTreated;

        /// <summary>
        /// Gets the selected size of the plank (cross-section).
        /// </summary>
        public Sizes Size
        {
            get => size;
            private set => size = value;
        }

        /// <summary>
        /// Gets the validated length of the plank in meters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the length is outside the allowed range defined by <see cref="MaterialValidationService.PlankValidator"/>.
        /// </exception>
        public double Length
        {
            get => length;
            private set => length = validator.ParseLength(value);
        }

        /// <summary>
        /// Gets a value indicating whether the plank has been treated against insects.
        /// </summary>
        public bool InsectTreated
        {
            get => insectTreated;
            private set => insectTreated = value;
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public Plank() : base(0, "Plank", 0, 0, MaterialTypes.Wood) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plank"/> class without a predefined ID.
        /// </summary>
        /// <param name="size">The size of the plank (cross-section type).</param>
        /// <param name="length">The length of the plank in meters.</param>
        /// <param name="insectTreated">Indicates whether the plank is insect-treated.</param>
        /// <param name="price">The unit price of the plank (net price).</param>
        /// <param name="vat">The VAT percentage applied to the plank.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for length or price.</exception>
        public Plank(Sizes size, double length, bool insectTreated, double price, double vat)
            : base("Plank", price, vat, MaterialTypes.Wood)
        {
            Size = size;
            Length = length;
            InsectTreated = insectTreated;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plank"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the plank.</param>
        /// <param name="size">The size of the plank (cross-section type).</param>
        /// <param name="length">The length of the plank in meters.</param>
        /// <param name="insectTreated">Indicates whether the plank is insect-treated.</param>
        /// <param name="price">The unit price of the plank (net price).</param>
        /// <param name="vat">The VAT percentage applied to the plank.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for length or price.</exception>
        public Plank(int id, Sizes size, double length, bool insectTreated, double price, double vat)
            : base(id, "Plank", price, vat, MaterialTypes.Wood)
        {
            Size = size;
            Length = length;
            InsectTreated = insectTreated;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { Size, Length, InsectTreated };

        /// <summary>
        /// Calculates the gross price of the plank (including VAT), multiplied by its length.
        /// </summary>
        /// <remarks>
        /// The gross price is calculated as:
        /// <c>base.GrossPrice() * Length</c>.
        /// </remarks>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice() * (double)length;
    }
}
