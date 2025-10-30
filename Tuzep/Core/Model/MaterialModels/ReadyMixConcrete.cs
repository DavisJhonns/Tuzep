using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a ready-mix concrete material.
    /// Inherits from the <see cref="Material"/> base class and includes specific
    /// properties such as cement content, consistency, and aggregate type.
    /// </summary>
    public class ReadyMixConcrete : Material
    {
        /// <summary>
        /// Validator used to verify ready-mix concrete–specific properties such as cement content.
        /// </summary>
        private readonly MaterialValidationService.ReadyMixConcreteValidator validator = new();

        /// <summary>
        /// Provides the icon for this material type.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the icon.
        /// </value>
        public override Bitmap Icon { get => Resources.MaterialIcons.ReadyMixConcrete; }

        /// <summary>
        /// Enumerates the available consistencies of ready-mix concrete.
        /// </summary>
        public enum Consistencies
        {
            /// <summary>
            /// Wet consistency – more fluid and easier to work with, often used for foundations and slabs.
            /// </summary>
            Wet,

            /// <summary>
            /// Dry consistency – stiffer mix, commonly used for structural or precast elements.
            /// </summary>
            Dry
        }

        /// <summary>
        /// Enumerates the aggregate types used in ready-mix concrete.
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Concrete made with small gravel aggregate.
            /// </summary>
            SmallGravel,

            /// <summary>
            /// Concrete made with large gravel aggregate.
            /// </summary>
            LargeGravel,

            /// <summary>
            /// Concrete made with crushed stone aggregate.
            /// </summary>
            CrushedStone
        }

        private double cementContent;
        private Consistencies consistency;
        private Types typeName;

        /// <summary>
        /// Gets the cement content of the concrete as a percentage.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the cement content is outside the valid range defined by <see cref="MaterialValidationService.ReadyMixConcreteValidator"/>.
        /// </exception>
        public double CementContent
        {
            get => cementContent;
            private set => cementContent = validator.ParseCementContent(value);
        }

        /// <summary>
        /// Gets the consistency of the ready-mix concrete (e.g., wet or dry).
        /// </summary>
        public Consistencies Consistency
        {
            get => consistency;
            private set => consistency = value;
        }

        /// <summary>
        /// Gets the type of aggregate used in the concrete mix.
        /// </summary>
        public Types TypeName
        {
            get => typeName;
            private set => typeName = value;
        }

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public ReadyMixConcrete() : base(0, "ReadyMixConcrete", 0, 0, MaterialTypes.Hard) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadyMixConcrete"/> class without a predefined ID.
        /// </summary>
        /// <param name="cementContent">The cement content percentage.</param>
        /// <param name="consistency">The consistency of the mix (wet or dry).</param>
        /// <param name="typeName">The type of aggregate used in the mix.</param>
        /// <param name="price">The unit price of the concrete (net price).</param>
        /// <param name="vat">The VAT percentage applied to the concrete.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for cement content or price.</exception>
        public ReadyMixConcrete(double cementContent, Consistencies consistency, Types typeName, double price, double vat)
            : base("ReadyMixConcrete", price, vat, MaterialTypes.Hard)
        {
            CementContent = cementContent;
            Consistency = consistency;
            TypeName = typeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadyMixConcrete"/> class with an existing ID.
        /// </summary>
        /// <param name="id">The unique identifier of the concrete.</param>
        /// <param name="cementContent">The cement content percentage.</param>
        /// <param name="consistency">The consistency of the mix (wet or dry).</param>
        /// <param name="typeName">The type of aggregate used in the mix.</param>
        /// <param name="price">The unit price of the concrete (net price).</param>
        /// <param name="vat">The VAT percentage applied to the concrete.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for cement content or price.</exception>
        public ReadyMixConcrete(int id, double cementContent, Consistencies consistency, Types typeName, double price, double vat)
            : base(id, "ReadyMixConcrete", price, vat, MaterialTypes.Hard)
        {
            CementContent = cementContent;
            Consistency = consistency;
            TypeName = typeName;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public override object GetUniqueProperties() => new { CementContent, Consistency, TypeName };

        /// <summary>
        /// Calculates the gross price of the ready-mix concrete (including VAT).
        /// In this class, it simply calls the base implementation.
        /// </summary>
        /// <returns>The gross price as a <see cref="double"/>.</returns>
        public override double GrossPrice() => base.GrossPrice();
    }
}
