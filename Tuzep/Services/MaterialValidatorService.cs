namespace Tuzep.Services
{
    /// <summary>
    /// Provides validation logic for various types of construction materials.
    /// Includes validators for general material data and specific material types (e.g., bricks, ytong, beams, etc.).
    /// </summary>
    public class MaterialValidationService
    {
        #region General

        /// <summary>
        /// Validates general material properties such as ID, name, unit price, and VAT.
        /// </summary>
        public class MaterialValidator
        {
            /// <summary>
            /// The minimum allowed unit price for a material.
            /// </summary>
            public readonly double minUnitPrice;

            /// <summary>
            /// The minimum allowed VAT percentage for a material.
            /// </summary>
            public readonly double minVatPercent;

            /// <summary>
            /// The maximum allowed VAT percentage for a material.
            /// </summary>
            public readonly double maxVatPercent;

            /// <summary>
            /// Initializes a new instance of the <see cref="MaterialValidator"/> class with default validation values.
            /// </summary>
            public MaterialValidator()
            {
                minUnitPrice = 0;
                minVatPercent = 0;
                maxVatPercent = 50;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MaterialValidator"/> class with custom validation values.
            /// </summary>
            /// <param name="minUnitPrice">The minimum unit price allowed.</param>
            /// <param name="minVatPercent">The minimum VAT percentage allowed.</param>
            /// <param name="maxVatPercent">The maximum VAT percentage allowed.</param>
            public MaterialValidator(double minUnitPrice, double minVatPercent, double maxVatPercent)
            {
                this.minUnitPrice = minUnitPrice;
                this.minVatPercent = minVatPercent;
                this.maxVatPercent = maxVatPercent;
            }

            /// <summary>
            /// Validates and parses an integer ID.
            /// </summary>
            /// <param name="input">The ID value to validate.</param>
            /// <returns>The validated ID value.</returns>
            /// <exception cref="ArgumentException">Thrown when the ID is negative.</exception>
            public int ParseId(int input)
            {
                if (input < 0)
                    throw new ArgumentException("ID cannot be negative.");
                return input;
            }

            /// <summary>
            /// Validates and parses a material name.
            /// </summary>
            /// <param name="input">The name string to validate.</param>
            /// <returns>The trimmed and validated material name.</returns>
            /// <exception cref="ArgumentException">Thrown when the name is null, empty, or whitespace.</exception>
            public string ParseName(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Name cannot be empty.");
                return input.Trim();
            }

            /// <summary>
            /// Validates and parses a unit price.
            /// </summary>
            /// <param name="unitPrice">The unit price to validate.</param>
            /// <returns>The validated unit price.</returns>
            /// <exception cref="ArgumentException">Thrown when the unit price is below the minimum threshold.</exception>
            public double ParseUnitPrice(double unitPrice)
            {
                if (unitPrice < minUnitPrice)
                    throw new ArgumentException("Unit price must be a positive number.");
                return unitPrice;
            }

            /// <summary>
            /// Validates and parses a VAT percentage.
            /// </summary>
            /// <param name="vat">The VAT value to validate.</param>
            /// <returns>The validated VAT value.</returns>
            /// <exception cref="ArgumentException">Thrown when the VAT is outside the allowed range.</exception>
            public double ParseVat(double vat)
            {
                if (vat < minVatPercent || vat > maxVatPercent)
                    throw new ArgumentException($"VAT must be between {minVatPercent} and {maxVatPercent}%.");
                return vat;
            }
        }

        #endregion

        #region Brick

        /// <summary>
        /// Validates properties related to bricks, such as thickness.
        /// </summary>
        public class BrickValidator
        {
            /// <summary>
            /// The minimum brick thickness (in cm).
            /// </summary>
            public readonly double minThickness;

            /// <summary>
            /// The maximum brick thickness (in cm).
            /// </summary>
            public readonly double maxThickness;

            /// <summary>
            /// Initializes a new instance of the <see cref="BrickValidator"/> class with default limits (10–40 cm).
            /// </summary>
            public BrickValidator()
            {
                minThickness = 10;
                maxThickness = 40;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BrickValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minThickness">Minimum allowed thickness (cm).</param>
            /// <param name="maxThickness">Maximum allowed thickness (cm).</param>
            public BrickValidator(double minThickness, double maxThickness)
            {
                this.minThickness = minThickness;
                this.maxThickness = maxThickness;
            }

            /// <summary>
            /// Validates the thickness of a brick.
            /// </summary>
            /// <param name="thickness">The thickness in centimeters.</param>
            /// <returns>The validated thickness value.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when the thickness is outside the allowed range.</exception>
            public double ParseThickness(double thickness)
            {
                if (thickness < minThickness || thickness > maxThickness)
                    throw new ArgumentOutOfRangeException(nameof(thickness), $"Brick thickness must be between {minThickness} and {maxThickness} cm.");
                return thickness;
            }
        }

        #endregion

        #region Ytong

        /// <summary>
        /// Validates Ytong (aerated concrete block) properties such as thickness and length.
        /// </summary>
        public class YtongValidator
        {
            /// <summary>
            /// The minimum Ytong thickness (in cm).
            /// </summary>
            public readonly double minThickness;

            /// <summary>
            /// The maximum Ytong thickness (in cm).
            /// </summary>
            public readonly double maxThickness;

            /// <summary>
            /// The minimum Ytong length (in cm).
            /// </summary>
            public readonly double minLength;

            /// <summary>
            /// The maximum Ytong length (in cm).
            /// </summary>
            public readonly double maxLength;

            /// <summary>
            /// Initializes a new instance of the <see cref="YtongValidator"/> class with default limits.
            /// </summary>
            public YtongValidator()
            {
                minThickness = 10;
                maxThickness = 30;
                minLength = 40;
                maxLength = 110;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="YtongValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minThickness">Minimum allowed thickness (cm).</param>
            /// <param name="maxThickness">Maximum allowed thickness (cm).</param>
            /// <param name="minLength">Minimum allowed length (cm).</param>
            /// <param name="maxLength">Maximum allowed length (cm).</param>
            public YtongValidator(double minThickness, double maxThickness, double minLength, double maxLength)
            {
                this.minThickness = minThickness;
                this.maxThickness = maxThickness;
                this.minLength = minLength;
                this.maxLength = maxLength;
            }

            /// <summary>
            /// Validates the thickness of a Ytong block.
            /// </summary>
            /// <param name="thickness">The thickness in centimeters.</param>
            /// <returns>The validated thickness value.</returns>
            /// <exception cref="ArgumentException">Thrown when the thickness is outside the allowed range.</exception>
            public double ParseThickness(double thickness)
            {
                if (thickness < minThickness || thickness > maxThickness)
                    throw new ArgumentException($"Ytong thickness must be between {minThickness} and {maxThickness} cm.");
                return thickness;
            }

            /// <summary>
            /// Validates the length of a Ytong block.
            /// </summary>
            /// <param name="length">The length in centimeters.</param>
            /// <returns>The validated length value.</returns>
            /// <exception cref="ArgumentException">Thrown when the length is outside the allowed range.</exception>
            public double ParseLength(double length)
            {
                if (length < minLength || length > maxLength)
                    throw new ArgumentException($"Ytong length must be between {minLength} and {maxLength} cm.");
                return length;
            }
        }

        #endregion

        #region Beam

        /// <summary>
        /// Validates properties related to beams, such as diameter and length.
        /// </summary>
        public class BeamValidator
        {
            /// <summary>
            /// The minimum diameter (in cm).
            /// </summary>
            public readonly double minDiameter;

            /// <summary>
            /// The maximum diameter (in cm).
            /// </summary>
            public readonly double maxDiameter;

            /// <summary>
            /// The minimum length (in meters).
            /// </summary>
            public readonly double minLength;

            /// <summary>
            /// The maximum length (in meters).
            /// </summary>
            public readonly double maxLength;

            /// <summary>
            /// Initializes a new instance of the <see cref="BeamValidator"/> class with default limits.
            /// </summary>
            public BeamValidator()
            {
                minDiameter = 10;
                maxDiameter = 25;
                minLength = 2;
                maxLength = 8;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BeamValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minDiameter">Minimum diameter (cm).</param>
            /// <param name="maxDiameter">Maximum diameter (cm).</param>
            /// <param name="minLength">Minimum length (m).</param>
            /// <param name="maxLength">Maximum length (m).</param>
            public BeamValidator(double minDiameter, double maxDiameter, double minLength, double maxLength)
            {
                this.minDiameter = minDiameter;
                this.maxDiameter = maxDiameter;
                this.minLength = minLength;
                this.maxLength = maxLength;
            }

            /// <summary>
            /// Validates the beam diameter.
            /// </summary>
            /// <param name="input">Diameter in centimeters.</param>
            /// <returns>The validated diameter value.</returns>
            /// <exception cref="ArgumentException">Thrown when the diameter is outside the allowed range.</exception>
            public double ParseDiameter(double input)
            {
                if (input < minDiameter || input > maxDiameter)
                    throw new ArgumentException($"Beam diameter must be between {minDiameter} and {maxDiameter} cm.");
                return input;
            }

            /// <summary>
            /// Validates the beam length.
            /// </summary>
            /// <param name="input">Length in meters.</param>
            /// <returns>The validated length value.</returns>
            /// <exception cref="ArgumentException">Thrown when the length is outside the allowed range.</exception>
            public double ParseLength(double input)
            {
                if (input < minLength || input > maxLength)
                    throw new ArgumentException($"Beam length must be between {minLength} and {maxLength} meters.");
                return input;
            }
        }

        #endregion

        #region Plank

        /// <summary>
        /// Validates properties related to planks, such as width and length.
        /// </summary>
        public class PlankValidator
        {
            /// <summary>
            /// The minimum plank width (in meters).
            /// </summary>
            public readonly double minWidth;

            /// <summary>
            /// The maximum plank width (in meters).
            /// </summary>
            public readonly double maxWidth;

            /// <summary>
            /// Initializes a new instance of the <see cref="PlankValidator"/> class with default limits (1–6 m).
            /// </summary>
            public PlankValidator()
            {
                minWidth = 1;
                maxWidth = 6;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PlankValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minWidth">Minimum allowed width (m).</param>
            /// <param name="maxWidth">Maximum allowed width (m).</param>
            public PlankValidator(double minWidth, double maxWidth)
            {
                this.minWidth = minWidth;
                this.maxWidth = maxWidth;
            }

            /// <summary>
            /// Validates the plank length.
            /// </summary>
            /// <param name="input">The length in meters.</param>
            /// <returns>The validated length value.</returns>
            /// <exception cref="ArgumentException">Thrown when the length is outside the allowed range.</exception>
            public double ParseLength(double input)
            {
                if (input < minWidth || input > maxWidth)
                    throw new ArgumentException($"Plank length must be between {minWidth} and {maxWidth} meters.");
                return input;
            }
        }

        #endregion

        #region ReadyMixConcrete

        /// <summary>
        /// Validates properties related to ready-mix concrete, such as cement content.
        /// </summary>
        public class ReadyMixConcreteValidator
        {
            /// <summary>
            /// The minimum cement content percentage.
            /// </summary>
            public readonly double minCementContent;

            /// <summary>
            /// The maximum cement content percentage.
            /// </summary>
            public readonly double maxCementContent;

            /// <summary>
            /// Initializes a new instance of the <see cref="ReadyMixConcreteValidator"/> class with default limits (10–32%).
            /// </summary>
            public ReadyMixConcreteValidator()
            {
                minCementContent = 10;
                maxCementContent = 32;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ReadyMixConcreteValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minCementContent">Minimum allowed cement content (%).</param>
            /// <param name="maxCementContent">Maximum allowed cement content (%).</param>
            public ReadyMixConcreteValidator(double minCementContent, double maxCementContent)
            {
                this.minCementContent = minCementContent;
                this.maxCementContent = maxCementContent;
            }

            /// <summary>
            /// Validates the cement content of the ready-mix concrete.
            /// </summary>
            /// <param name="input">Cement content in percent.</param>
            /// <returns>The validated cement content value.</returns>
            /// <exception cref="ArgumentException">Thrown when the cement content is outside the allowed range.</exception>
            public double ParseCementContent(double input)
            {
                if (input < minCementContent || input > maxCementContent)
                    throw new ArgumentException($"Cement content must be between {minCementContent}% and {maxCementContent}%.");
                return input;
            }
        }

        #endregion

        #region CrushedStone

        /// <summary>
        /// Validates crushed stone properties such as grain size and weight (density).
        /// </summary>
        public class CrushedStoneValidator
        {
            /// <summary>
            /// The minimum grain size (in mm).
            /// </summary>
            public readonly double minGrainSize;

            /// <summary>
            /// The maximum grain size (in mm).
            /// </summary>
            public readonly double maxGrainSize;

            /// <summary>
            /// The minimum material weight (in kg/m³).
            /// </summary>
            public readonly double minWeight;

            /// <summary>
            /// The maximum material weight (in kg/m³).
            /// </summary>
            public readonly double maxWeight;

            /// <summary>
            /// Initializes a new instance of the <see cref="CrushedStoneValidator"/> class with default limits.
            /// </summary>
            public CrushedStoneValidator()
            {
                minGrainSize = 5;
                maxGrainSize = 40;
                minWeight = 1200;
                maxWeight = 2400;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CrushedStoneValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minGrainSize">Minimum allowed grain size (mm).</param>
            /// <param name="maxGrainSize">Maximum allowed grain size (mm).</param>
            /// <param name="minWeight">Minimum allowed weight (kg/m³).</param>
            /// <param name="maxWeight">Maximum allowed weight (kg/m³).</param>
            public CrushedStoneValidator(double minGrainSize, double maxGrainSize, double minWeight, double maxWeight)
            {
                this.minGrainSize = minGrainSize;
                this.maxGrainSize = maxGrainSize;
                this.minWeight = minWeight;
                this.maxWeight = maxWeight;
            }

            /// <summary>
            /// Validates the grain size of crushed stone.
            /// </summary>
            /// <param name="input">Grain size in millimeters.</param>
            /// <returns>The validated grain size.</returns>
            /// <exception cref="ArgumentException">Thrown when the grain size is outside the allowed range.</exception>
            public double ParseGrainSize(double input)
            {
                if (input < minGrainSize || input > maxGrainSize)
                    throw new ArgumentException($"Grain size must be between {minGrainSize} and {maxGrainSize} mm.");
                return input;
            }

            /// <summary>
            /// Validates the material weight (density) of crushed stone.
            /// </summary>
            /// <param name="input">Weight in kilograms per cubic meter (kg/m³).</param>
            /// <returns>The validated weight value.</returns>
            /// <exception cref="ArgumentException">Thrown when the weight is outside the allowed range.</exception>
            public double ParseWeight(double input)
            {
                if (input < minWeight || input > maxWeight)
                    throw new ArgumentException($"Weight must be between {minWeight} and {maxWeight} kg/m³.");
                return input;
            }
        }

        #endregion

        #region RockWool

        /// <summary>
        /// Validates rock wool insulation properties such as thickness.
        /// </summary>
        public class RockWoolValidator
        {
            /// <summary>
            /// The minimum rock wool thickness (in cm).
            /// </summary>
            public readonly double minThickness;

            /// <summary>
            /// The maximum rock wool thickness (in cm).
            /// </summary>
            public readonly double maxThickness;

            /// <summary>
            /// Initializes a new instance of the <see cref="RockWoolValidator"/> class with default limits (5–20 cm).
            /// </summary>
            public RockWoolValidator()
            {
                minThickness = 5;
                maxThickness = 20;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RockWoolValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minThickness">Minimum allowed thickness (cm).</param>
            /// <param name="maxThickness">Maximum allowed thickness (cm).</param>
            public RockWoolValidator(double minThickness, double maxThickness)
            {
                this.minThickness = minThickness;
                this.maxThickness = maxThickness;
            }

            /// <summary>
            /// Validates the thickness of rock wool insulation.
            /// </summary>
            /// <param name="input">Thickness in centimeters.</param>
            /// <returns>The validated thickness value.</returns>
            /// <exception cref="ArgumentException">Thrown when the thickness is outside the allowed range.</exception>
            public double ParseThickness(double input)
            {
                if (input < minThickness || input > maxThickness)
                    throw new ArgumentException($"Rock wool thickness must be between {minThickness} and {maxThickness} cm.");
                return input;
            }
        }

        #endregion

        #region Styrofoam

        /// <summary>
        /// Validates styrofoam insulation properties such as thickness.
        /// </summary>
        public class StyrofoamValidator
        {
            /// <summary>
            /// The minimum styrofoam thickness (in cm).
            /// </summary>
            public readonly double minThickness;

            /// <summary>
            /// The maximum styrofoam thickness (in cm).
            /// </summary>
            public readonly double maxThickness;

            /// <summary>
            /// Initializes a new instance of the <see cref="StyrofoamValidator"/> class with default limits (5–20 cm).
            /// </summary>
            public StyrofoamValidator()
            {
                minThickness = 5;
                maxThickness = 20;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StyrofoamValidator"/> class with custom limits.
            /// </summary>
            /// <param name="minThickness">Minimum allowed thickness (cm).</param>
            /// <param name="maxThickness">Maximum allowed thickness (cm).</param>
            public StyrofoamValidator(double minThickness, double maxThickness)
            {
                this.minThickness = minThickness;
                this.maxThickness = maxThickness;
            }

            /// <summary>
            /// Validates the thickness of styrofoam insulation.
            /// </summary>
            /// <param name="input">Thickness in centimeters.</param>
            /// <returns>The validated thickness value.</returns>
            /// <exception cref="ArgumentException">Thrown when the thickness is outside the allowed range.</exception>
            public double ParseThickness(double input)
            {
                if (input < minThickness || input > maxThickness)
                    throw new ArgumentException($"Styrofoam thickness must be between {minThickness} and {maxThickness} cm.");
                return input;
            }
        }

        #endregion
    }
}
