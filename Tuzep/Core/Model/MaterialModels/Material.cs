using Tuzep.Services;

namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Represents a base class for all material entities.
    /// Provides common properties such as ID, name, price, VAT, and type, along with basic validation.
    /// </summary>
    public abstract class Material
    {
        /// <summary>
        /// Validator instance used to validate general material properties.
        /// </summary>
        private readonly MaterialValidationService.MaterialValidator validator = new();

        private const double vatDivisor = 100;

        private int id;
        private string name;
        private double unitPrice;
        private double vatPercent;
        private MaterialTypes materialType;

        /// <summary>
        /// Gets the unique identifier of the material.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the ID is negative.</exception>
        public int Id
        {
            get => id;
            protected set => id = validator.ParseId(value);
        }

        /// <summary>
        /// Gets the validated name of the material.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when name is empty or whitespace.</exception>
        public string Name
        {
            get => name;
            protected set => name = validator.ParseName(value);
        }

        /// <summary>
        /// Gets the validated unit price of the material.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the unit price is less than or equal to zero.</exception>
        public double UnitPrice
        {
            get => unitPrice;
            protected set => unitPrice = validator.ParseUnitPrice(value);
        }

        /// <summary>
        /// Gets the validated VAT percentage of the material.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when VAT is outside the allowed range (0–50%).</exception>
        public double VatPercent
        {
            get => vatPercent;
            protected set => vatPercent = validator.ParseVat(value);
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        public MaterialTypes MaterialType
        {
            get => materialType;
            protected set => materialType = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class for a new material (ID defaults to 0).
        /// </summary>
        /// <param name="name">The material name.</param>
        /// <param name="unitPrice">The material unit price (net price).</param>
        /// <param name="vatPercent">The VAT percentage (0–50).</param>
        /// <param name="type">The material type.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any property.</exception>
        protected Material(string name, double unitPrice, double vatPercent, MaterialTypes type)
        {
            Id = 0;
            Name = name;
            UnitPrice = unitPrice;
            VatPercent = vatPercent;
            MaterialType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class with an existing database ID.
        /// </summary>
        /// <param name="id">The unique material ID.</param>
        /// <param name="name">The material name.</param>
        /// <param name="unitPrice">The material unit price (net price).</param>
        /// <param name="vatPercent">The VAT percentage (0–50).</param>
        /// <param name="type">The material type.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails for any property.</exception>
        protected Material(int id, string name, double unitPrice, double vatPercent, MaterialTypes type)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            VatPercent = vatPercent;
            MaterialType = type;
        }

        /// <summary>
        /// Calculates the gross price of the material (including VAT).
        /// </summary>
        /// <returns>The gross unit price as a <see cref="double"/>.</returns>
        public virtual double GrossPrice()
        {
            double vatMultiplier = 1 + ((double)VatPercent / vatDivisor);
            return UnitPrice * vatMultiplier;
        }

        /// <summary>
        /// Returns an anonymous object that represents the unique properties of this instance,
        /// </summary>
        public abstract object GetUniqueProperties();

        /// <summary>
        /// Gets the default icon associated with the material.
        /// </summary>
        /// <value>
        /// A <see cref="Bitmap"/> representing the material's icon.
        /// </value>
        public virtual Bitmap Icon => Resources.MaterialIcons.Unknown;

        /// <summary>
        /// Returns a string representation of the material.
        /// </summary>
        /// <returns>A formatted string containing the material name, type, net price, and VAT percentage.</returns>
        public override string ToString() => $"{Name} ({MaterialType}) - [{UnitPrice} Ft.] - {VatPercent}% VAT";
    }
}
