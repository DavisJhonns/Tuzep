using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a Ytong (aerated concrete) block material.
    /// Inherits from the <see cref="Material"/> base class and includes additional
    /// properties specific to Ytong blocks such as type, thickness, and length.
    /// </summary>
    public class Ytong : Material
    {
        /// <summary>
        /// Validator used to verify Ytong-specific dimensions such as thickness and length.
        /// </summary>
        private readonly MaterialValidationService.YtongValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.Ytong; }

        /// <summary>
        /// Enumerates the available Ytong block types.
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Smooth Ytong block without interlocking edges.
            /// </summary>
            Smooth,

            /// <summary>
            /// Ytong block with interlocking edges (nut and feather system).
            /// </summary>
            NutAndFeather
        }

        private Types typeName;
        private double thickness;
        private double length;

        /// <summary>
        /// Gets the type of the Ytong block (e.g., smooth or nut and feather).
        /// </summary>
        public Types TypeName
        {
            get => typeName;
            private set => typeName = value;
        }

        /// <summary>
        /// Gets the validated thickness of the Ytong block in centimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the thickness is outside the allowed range defined by <see cref="MaterialValidationService.YtongValidator"/>.
        /// </exception>
        public double Thickness
        {
            get => thickness;
            private set => thickness = validator.ParseThickness(value);
        }

        /// <summary>
        /// Gets the validated length of the Ytong block in centimeters.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the length is outside the allowed range defined by <see cref="MaterialValidationService.YtongValidator"/>.
        /// </exception>
        public double Length
        {
            get => length;
            private set => length = validator.ParseLength(value);
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public Ytong() : base(0, "Ytong", 0, 0, MaterialTypes.Hard) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ytong"/> class without a predefined ID.
        /// </summary>
        /// <param name="typeName">The type of the Ytong block (e.g., smooth or nut and feather).</param>
        /// <param name="thickness">The thickness of the block in centimeters.</param>
        /// <param name="length">The length of the block in centimeters.</param>
        /// <param name="price">The unit price of the block (net price).</param>
        /// <param name="vat">The VAT percentage applied to the block.</param>
        /// <exception cref="ArgumentException">Thrown when any validation fails.</exception>
        public Ytong(Types typeName, double thickness, double length, double price, double vat)
            : base("Ytong", price, vat, MaterialTypes.Hard)
        {
            TypeName = typeName;
            Thickness = thickness;
            Length = length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ytong"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Ytong block.</param>
        /// <param name="typeName">The type of the Ytong block (e.g., smooth or nut and feather).</param>
        /// <param name="thickness">The thickness of the block in centimeters.</param>
        /// <param name="length">The length of the block in centimeters.</param>
        /// <param name="price">The unit price of the block (net price).</param>
        /// <param name="vat">The VAT percentage applied to the block.</param>
        /// <exception cref="ArgumentException">Thrown when any validation fails.</exception>
        public Ytong(int id, Types typeName, double thickness, double length, double price, double vat)
            : base(id, "Ytong", price, vat, MaterialTypes.Hard)
        {
            TypeName = typeName;
            Thickness = thickness;
            Length = length;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { TypeName, Thickness, Length };

        /// <summary>
        /// Calculates the gross price of the ytong (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
