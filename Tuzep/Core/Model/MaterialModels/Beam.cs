using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a beam material, typically made of wood.
    /// Inherits from the <see cref="Material"/> base class and includes specific properties
    /// such as diameter, length, and insect treatment status.
    /// </summary>
    public class Beam : Material
    {
        /// <summary>
        /// Validator used to verify beam-specific properties such as diameter and length.
        /// </summary>
        private readonly MaterialValidationService.BeamValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.Beam; }

        private double diameter;
        private double length;
        private bool insectTreated;

        /// <summary>
        /// Gets the validated diameter of the beam in centimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the diameter is outside the allowed range defined by <see cref="MaterialValidationService.BeamValidator"/>.
        /// </exception>
        public double Diameter
        {
            get => diameter;
            private set => diameter = validator.ParseDiameter(value);
        }

        /// <summary>
        /// Gets the validated length of the beam in meters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the length is outside the allowed range defined by <see cref="MaterialValidationService.BeamValidator"/>.
        /// </exception>
        public double Length
        {
            get => length;
            private set => length = validator.ParseLength(value);
        }

        /// <summary>
        /// Gets a value indicating whether the beam has been treated against insects.
        /// </summary>
        public bool InsectTreated
        {
            get => insectTreated;
            private set => insectTreated = value;
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public Beam() : base(0, "Beam", 0, 0, MaterialTypes.Wood) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beam"/> class without a predefined ID.
        /// </summary>
        /// <param name="diameter">The diameter of the beam in centimeters.</param>
        /// <param name="length">The length of the beam in meters.</param>
        /// <param name="insectTreated">Indicates whether the beam is insect-treated.</param>
        /// <param name="price">The unit price of the beam (net price).</param>
        /// <param name="vat">The VAT percentage applied to the beam.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any dimension or price.</exception>
        public Beam(double diameter, double length, bool insectTreated, double price, double vat)
            : base("Beam", price, vat, MaterialTypes.Wood)
        {
            Diameter = diameter;
            Length = length;
            InsectTreated = insectTreated;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beam"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the beam.</param>
        /// <param name="diameter">The diameter of the beam in centimeters.</param>
        /// <param name="length">The length of the beam in meters.</param>
        /// <param name="insectTreated">Indicates whether the beam is insect-treated.</param>
        /// <param name="price">The unit price of the beam (net price).</param>
        /// <param name="vat">The VAT percentage applied to the beam.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any dimension or price.</exception>
        public Beam(int id, double diameter, double length, bool insectTreated, double price, double vat)
            : base(id, "Beam", price, vat, MaterialTypes.Wood)
        {
            Diameter = diameter;
            Length = length;
            InsectTreated = insectTreated;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { Diameter, Length, InsectTreated };

        /// <summary>
        /// Calculates the gross price of the beam (including VAT) multiplied by its length.
        /// </summary>
        /// <remarks>
        /// The gross price is calculated as:
        /// <c>base.GrossPrice() * Length</c>.
        /// </remarks>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice() * (double)length;
    }
}
