namespace Tuzep.Core.Model.MaterialModels
{
    /// <summary>
    /// Provides a centralized definition of all available material types
    /// and a mapping between material type identifiers and their corresponding
    /// concrete <see cref="Material"/> class implementations.
    /// </summary>
    public class AvaibleMaterials
    {
        /// <summary>
        /// Enumerates all supported material types in the system.
        /// </summary>
        /// <remarks>
        /// Each enum value corresponds to a specific material implementation class
        /// (for example, <see cref="MaterialTypes.Brick"/> maps to <see cref="Brick"/>).
        /// </remarks>
        public enum MaterialTypes
        {
            /// <summary>Represents a standard fired brick material.</summary>
            Brick,

            /// <summary>Represents a Ytong (aerated concrete) building block.</summary>
            Ytong,

            /// <summary>Represents a wooden beam.</summary>
            Beam,

            /// <summary>Represents a wooden plank.</summary>
            Plank,

            /// <summary>Represents ready-mix concrete material.</summary>
            ReadyMixConcrete,

            /// <summary>Represents crushed stone material (e.g., gravel or aggregate).</summary>
            CrushedStone,

            /// <summary>Represents rock wool insulation material.</summary>
            RockWool,

            /// <summary>Represents styrofoam (polystyrene) insulation material.</summary>
            Styrofoam
        }

        /// <summary>
        /// A lookup table that maps each <see cref="MaterialTypes"/> value
        /// to its corresponding concrete <see cref="Type"/> derived from <see cref="Material"/>.
        /// </summary>
        /// <remarks>
        /// This mapping allows the application to dynamically create material instances
        /// or perform type-based reflection without hardcoding type names.
        /// <para>Example usage:</para>
        /// <code>
        /// var type = AvaibleMaterials.MaterialTypeMap[AvaibleMaterials.MaterialTypes.Brick];
        /// var brickInstance = (Material)Activator.CreateInstance(type)!;
        /// </code>
        /// </remarks>
        public static readonly Dictionary<MaterialTypes, Type> MaterialTypeMap = new()
        {
            { MaterialTypes.Brick, typeof(Brick) },
            { MaterialTypes.Ytong, typeof(Ytong) },
            { MaterialTypes.Beam, typeof(Beam) },
            { MaterialTypes.Plank, typeof(Plank) },
            { MaterialTypes.ReadyMixConcrete, typeof(ReadyMixConcrete) },
            { MaterialTypes.CrushedStone, typeof(CrushedStone) },
            { MaterialTypes.RockWool, typeof(RockWool) },
            { MaterialTypes.Styrofoam, typeof(Styrofoam) }
        };
    }
}