using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a crushed stone material.
    /// Inherits from the <see cref="Material"/> base class and includes
    /// specific properties such as grain size, decorative use, and weight.
    /// </summary>
    public class CrushedStone : Material
    {
        /// <summary>
        /// Validator used to verify crushed stone–specific properties such as grain size and weight.
        /// </summary>
        private readonly MaterialValidationService.CrushedStoneValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.ChrushedStone; }

        private double grainSize;
        private bool decorative;
        private double weight;

        /// <summary>
        /// Gets or sets the validated grain size of the crushed stone in millimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the grain size is outside the valid range defined by
        /// <see cref="MaterialValidationService.CrushedStoneValidator"/>.
        /// </exception>
        public double GrainSize
        {
            get => grainSize;
            set => grainSize = validator.ParseGrainSize(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the crushed stone is decorative.
        /// </summary>
        /// <remarks>
        /// Decorative crushed stone is typically used for landscaping or aesthetic purposes,
        /// while non-decorative stone is used for construction and structural applications.
        /// </remarks>
        public bool Decorative
        {
            get => decorative;
            set => decorative = value;
        }

        /// <summary>
        /// Gets or sets the validated weight of the crushed stone in kilograms per cubic meter (kg/m³).
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the weight is outside the valid range defined by
        /// <see cref="MaterialValidationService.CrushedStoneValidator"/>.
        /// </exception>
        public double Weight
        {
            get => weight;
            set => weight = validator.ParseWeight(value);
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public CrushedStone() : base(0, "CrushedStone", 0, 0, MaterialTypes.Hard) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrushedStone"/> class without a predefined ID.
        /// </summary>
        /// <param name="grainSize">The grain size of the crushed stone in millimeters.</param>
        /// <param name="decorative">Indicates whether the crushed stone is decorative.</param>
        /// <param name="weight">The weight of the crushed stone in kilograms per cubic meter (kg/m³).</param>
        /// <param name="price">The unit price of the crushed stone (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any physical property or price.</exception>
        public CrushedStone(double grainSize, bool decorative, double weight, double price, double vat)
            : base("CrushedStone", price, vat, MaterialTypes.Hard)
        {
            GrainSize = grainSize;
            Decorative = decorative;
            Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrushedStone"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the crushed stone entry.</param>
        /// <param name="grainSize">The grain size of the crushed stone in millimeters.</param>
        /// <param name="decorative">Indicates whether the crushed stone is decorative.</param>
        /// <param name="weight">The weight of the crushed stone in kilograms per cubic meter (kg/m³).</param>
        /// <param name="price">The unit price of the crushed stone (net price).</param>
        /// <param name="vat">The VAT percentage applied to the material.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any physical property or price.</exception>
        public CrushedStone(int id, double grainSize, bool decorative, double weight, double price, double vat)
            : base(id, "CrushedStone", price, vat, MaterialTypes.Hard)
        {
            GrainSize = grainSize;
            Decorative = decorative;
            Weight = weight;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { GrainSize, Decorative, Weight };

        /// <summary>
        /// Calculates the gross price of the crushed stone (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
