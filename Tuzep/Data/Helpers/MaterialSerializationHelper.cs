using System.Text.Json;
using System.Text.Json.Serialization;
using Tuzep.Core.Model.MaterialModels;

namespace Tuzep.Data.Helpers
{
    public static class MaterialSerializationHelper
    {
        /// <summary>
        /// Provides default JSON serialization settings used across the application.
        /// </summary>
        /// <remarks>
        /// - <see cref="JsonSerializerOptions.WriteIndented"/> is set to <c>false</c> to produce compact JSON output.<br/>
        /// - Adds a <see cref="JsonStringEnumConverter"/> so that enumeration values are serialized and deserialized as strings
        ///   instead of their numeric values, improving readability and data portability.
        /// </remarks>
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            /// <summary>
            /// Disables indentation for compact JSON output (minified format).
            /// </summary>
            WriteIndented = false,

            /// <summary>
            /// Adds a converter that serializes enum values as strings rather than integers.
            /// </summary>
            Converters = { new JsonStringEnumConverter() }
        };

        /// <summary>
        /// Data Transfer Object (DTO) representing a material and its associated data
        /// for serialization, deserialization, or transport between layers.
        /// </summary>
        public struct MaterialDTO
        {
            private Dictionary<string, JsonElement> Specification { get; set; }

            /// <summary>
            /// Gets or sets the unique identifier of the material.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name of the material type (e.g., "Brick", "Beam").
            /// Used to determine the concrete material class during deserialization.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the unit price of the material (net price without VAT).
            /// </summary>
            public double UnitPrice { get; set; }

            /// <summary>
            /// Gets or sets the VAT percentage applied to the material.
            /// </summary>
            public double VatPercent { get; set; }

            /// <summary>
            /// Sets the material's specification from a JSON string.
            /// </summary>
            /// <param name="json">A JSON string representing a dictionary of material-specific properties.</param>
            /// <remarks>
            /// The JSON should contain key-value pairs where keys are property names and values are JSON representations 
            /// of the corresponding material-specific values.  
            /// For example:
            /// <code>
            /// {
            ///   "Form": "Solid",
            ///   "Thickness": 11.5
            /// }
            /// </code>
            /// </remarks>
            public void SetSpecification(string json) => this.Specification = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            /// <summary>
            /// Gets the material's specification as a dictionary of JSON elements.
            /// </summary>
            /// <returns>
            /// A dictionary where the keys are property names and the values are <see cref="JsonElement"/> instances
            /// representing the property values.
            /// </returns>
            public Dictionary<string, JsonElement> GetSpecification() => this.Specification;

            public string GetSpecificationAsJson()
                => JsonSerializer.Serialize(Specification, JsonOptions);
        }

        /// <summary>
        /// Serializes a <see cref="Material"/> into a <see cref="MaterialDTO"/>,
        /// converting enums to readable strings.
        /// </summary>
        public static MaterialDTO SerializeMaterial(Material material)
        {
            var dto = new MaterialDTO
            {
                Id = material.Id,
                Name = material.GetType().Name,
                UnitPrice = material.UnitPrice,
                VatPercent = material.VatPercent
            };

            var specJson = JsonSerializer.Serialize(material.GetUniqueProperties(), JsonOptions);
            dto.SetSpecification(specJson);

            return dto;
        }

        /// <summary>
        /// Deserializes a <see cref="MaterialDTO"/> into a strongly typed <see cref="Material"/> instance
        /// based on the <see cref="MaterialDTO.Name"/> property.
        /// </summary>
        /// <param name="dto">The <see cref="MaterialDTO"/> containing the material type, price, VAT, and specification data.</param>
        /// <returns>
        /// A strongly typed <see cref="Material"/> instance (e.g., <see cref="Brick"/>, <see cref="Ytong"/>, <see cref="Beam"/>, etc.)
        /// initialized with the values from the DTO.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="dto.Name"/> does not match any known material type.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The method uses a switch expression on <paramref name="dto.Name"/> to determine which material class to instantiate.
        /// It extracts material-specific properties from <paramref name="dto.Specification"/> using
        /// <see cref="GetValue{T}(Dictionary{string, JsonElement}, string)"/>.
        /// </para>
        /// <para>
        /// Supported material types include:
        /// <list type="table">
        /// <item>Brick</item>
        /// <item>Ytong</item>
        /// <item>Beam</item>
        /// <item>Plank</item>
        /// <item>ReadyMixConcrete</item>
        /// <item>CrushedStone</item>
        /// <item>RockWool</item>
        /// <item>Styrofoam</item>
        /// </list>
        /// </para>
        /// </remarks>
        public static Material DeserializeMaterial(MaterialDTO dto)
        {
            return dto.Name switch
            {
                nameof(Brick) => DeserializeBrick(dto),

                nameof(Ytong) => DeserializeYtong(dto),

                nameof(Beam) => DeserializeBeam(dto),

                nameof(Plank) => DeserializePlank(dto),

                nameof(ReadyMixConcrete) => DeserializeReadyMixConcrete(dto),

                nameof(CrushedStone) => DeserializeCrushedStone(dto),

                nameof(RockWool) => DeserializeRockWool(dto),

                nameof(Styrofoam) => DeserializeStyrofoam(dto),

                _ => throw new InvalidOperationException($"Unknown material type: {dto.Name}")
            };
        }

        /// <summary>
        /// Retrieves and deserializes a value of type <typeparamref name="T"/> from a dictionary of <see cref="JsonElement"/> objects.
        /// </summary>
        /// <typeparam name="T">The target type to deserialize the value into.</typeparam>
        /// <param name="dict">The dictionary containing <see cref="JsonElement"/> values keyed by string.</param>
        /// <param name="key">The key of the value to retrieve from the dictionary.</param>
        /// <returns>The value associated with the specified key, deserialized as type <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the dictionary does not contain the specified key.</exception>
        /// <remarks>
        /// This method is useful when deserializing dynamic JSON objects into strongly typed values.
        /// </remarks>
        private static T GetValue<T>(Dictionary<string, JsonElement> dict, string key)
        {
            if (!dict.TryGetValue(key, out var value))
                throw new InvalidOperationException($"Key '{key}' not found.");
            return value.Deserialize<T>(JsonOptions)!;
        }

        private static Brick DeserializeBrick(MaterialDTO dto)
        {
            return new Brick(
                    dto.Id,
                    Enum.Parse<Brick.Forms>(GetValue<string>(dto.GetSpecification(), nameof(Brick.Form))),
                    GetValue<double>(dto.GetSpecification(), nameof(Brick.Thickness)),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static Ytong DeserializeYtong(MaterialDTO dto)
        {
            return new Ytong(
                    dto.Id,
                    Enum.Parse<Ytong.Types>(GetValue<string>(dto.GetSpecification(), nameof(Ytong.TypeName))),
                    GetValue<double>(dto.GetSpecification(), nameof(Ytong.Thickness)),
                    GetValue<double>(dto.GetSpecification(), nameof(Ytong.Length)),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static Beam DeserializeBeam(MaterialDTO dto)
        {
            return new Beam(
                    dto.Id,
                    GetValue<double>(dto.GetSpecification(), nameof(Beam.Diameter)),
                    GetValue<double>(dto.GetSpecification(), nameof(Beam.Length)),
                    GetValue<bool>(dto.GetSpecification(), nameof(Beam.InsectTreated)),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static Plank DeserializePlank(MaterialDTO dto)
        {
            return new Plank(
                    dto.Id,
                    Enum.Parse<Plank.Sizes>(GetValue<string>(dto.GetSpecification(), nameof(Plank.Size))),
                    GetValue<double>(dto.GetSpecification(), nameof(Plank.Length)),
                    GetValue<bool>(dto.GetSpecification(), nameof(Plank.InsectTreated)),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static ReadyMixConcrete DeserializeReadyMixConcrete(MaterialDTO dto)
        {
            return new ReadyMixConcrete(
                    dto.Id,
                    GetValue<double>(dto.GetSpecification(), nameof(ReadyMixConcrete.CementContent)),
                    Enum.Parse<ReadyMixConcrete.Consistencies>(GetValue<string>(dto.GetSpecification(), nameof(ReadyMixConcrete.Consistency))),
                    Enum.Parse<ReadyMixConcrete.Types>(GetValue<string>(dto.GetSpecification(), nameof(ReadyMixConcrete.TypeName))),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static CrushedStone DeserializeCrushedStone(MaterialDTO dto)
        {
            return new CrushedStone(
                    dto.Id,
                    GetValue<double>(dto.GetSpecification(), nameof(CrushedStone.GrainSize)),
                    GetValue<bool>(dto.GetSpecification(), nameof(CrushedStone.Decorative)),
                    GetValue<double>(dto.GetSpecification(), nameof(CrushedStone.Weight)),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static RockWool DeserializeRockWool(MaterialDTO dto)
        {
            return new RockWool(
                    dto.Id,
                    GetValue<double>(dto.GetSpecification(), nameof(RockWool.Thickness)),
                    Enum.Parse<RockWool.Forms>(GetValue<string>(dto.GetSpecification(), nameof(RockWool.Form))),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
        private static Styrofoam DeserializeStyrofoam(MaterialDTO dto)
        {
            return new Styrofoam(
                    dto.Id,
                    GetValue<double>(dto.GetSpecification(), nameof(Styrofoam.Thickness)),
                    GetValue<bool>(dto.GetSpecification(), nameof(Styrofoam.StepResistant)),
                    Enum.Parse<Styrofoam.BoardSizes>(GetValue<string>(dto.GetSpecification(), nameof(Styrofoam.BoardSize))),
                    dto.UnitPrice,
                    dto.VatPercent);
        }
    }
}
