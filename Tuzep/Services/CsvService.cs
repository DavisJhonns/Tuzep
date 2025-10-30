using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tuzep.Core.Model.MaterialModels;

namespace Tuzep.Services
{
    /// <summary>
    /// Provides export and import functionality for Material objects in CSV format.
    /// Uses a switch-based approach to explicitly serialize and deserialize each material type.
    /// </summary>
    public static class CsvService
    {
        /// <summary>
        /// Exports a material instance to a CSV file using SaveFileDialog.
        /// </summary>
        /// <param name="exportMaterial">The material to export.</param>
        public static void ExportCSV(Material exportMaterial)
        {
            if (exportMaterial == null)
            {
                MessageBox.Show("No material to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using SaveFileDialog saveDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Export Material to CSV",
                FileName = $"{exportMaterial.Name}_export.csv"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var csvContent = BuildCsv(exportMaterial);
                File.WriteAllText(saveDialog.FileName, csvContent, Encoding.UTF8);

                MessageBox.Show("Material exported successfully.", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Imports a material from a CSV file using OpenFileDialog.
        /// </summary>
        /// <returns>A new Material instance built from the CSV data.</returns>
        public static Material ImportCSV()
        {
            using OpenFileDialog openDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Import Material from CSV"
            };

            if (openDialog.ShowDialog() != DialogResult.OK)
                return null;

            try
            {
                var lines = File.ReadAllLines(openDialog.FileName, Encoding.UTF8);
                if (lines.Length < 2)
                    throw new InvalidDataException("CSV must contain header and at least one data line.");

                var headers = lines[0].Split(';');
                var values = lines[1].Split(';');

                if (headers.Length != values.Length)
                    throw new InvalidDataException("CSV header and value count mismatch.");

                var dict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Length; i++)
                    dict[headers[i].Trim()] = values[i].Trim();

                return ParseMaterial(dict);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #region HELPERS

        private static string BuildCsv(Material material)
        {
            StringBuilder sb = new();
            string header;
            string data;

            switch (material)
            {
                case Brick mat when material is Brick:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Form;Thickness";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.Form};{mat.Thickness}";
                    break;

                case Ytong mat when material is Ytong:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Density;BlockSize";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.TypeName};{mat.Thickness}";
                    break;

                case Beam mat when material is Beam:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Diameter;Length;InsectTreated";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.Diameter};{mat.Length};{mat.InsectTreated}";
                    break;

                case Plank mat when material is Plank:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Size;Length;InsectTreated";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.Size};{mat.Length};{mat.InsectTreated}";
                    break;

                case ReadyMixConcrete mat when material is ReadyMixConcrete:
                    header = "Name;UnitPrice;VatPercent;MaterialType;CementContent;Consistency;TypeName";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.CementContent};{mat.Consistency};{mat.TypeName}";
                    break;

                case CrushedStone mat when material is CrushedStone:
                    header = "Name;UnitPrice;VatPercent;MaterialType;GrainSize;Decorative;Weight";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.GrainSize};{mat.Decorative};{mat.Weight}";
                    break;

                case RockWool mat when material is RockWool:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Thickness;Form";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.Thickness};{mat.Form}";
                    break;

                case Styrofoam mat when material is Styrofoam:
                    header = "Name;UnitPrice;VatPercent;MaterialType;Thickness;StepResistant;BoardSize";
                    data = $"{mat.Name};{mat.UnitPrice};{mat.VatPercent};{mat.MaterialType};{mat.Thickness};{mat.StepResistant};{mat.BoardSize}";
                    break;

                default:
                    throw new NotSupportedException($"Unsupported material type: {material.MaterialType}");
            }

            sb.AppendLine(header);
            sb.AppendLine(data);
            sb.AppendLine();
            return sb.ToString();
        }

        private static Material ParseMaterial(Dictionary<string, string> dict)
        {
            if (!dict.TryGetValue("Name", out string materialTypeName))
                throw new InvalidDataException("CSV missing 'MaterialType' column.");

            if (!Enum.TryParse(materialTypeName, out AvaibleMaterials.MaterialTypes type))
                throw new InvalidDataException($"Unknown material type: {materialTypeName}");

            switch (type)
            {
                case AvaibleMaterials.MaterialTypes.Brick:
                    return new Brick(Enum.Parse<Brick.Forms>(dict["Form"]),
                        double.Parse(dict["Thickness"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"]));

                case AvaibleMaterials.MaterialTypes.Ytong:
                    return new Ytong(Enum.Parse<Ytong.Types>(dict["TypeName"]),
                        double.Parse(dict["Thickness"]),
                        double.Parse(dict["Length"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.Beam:
                    return new Beam(double.Parse(dict["Diameter"]),
                        double.Parse(dict["Length"]),
                        bool.Parse(dict["InsectTreated"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.Plank:
                    return new Plank(Enum.Parse<Plank.Sizes>(dict["Size"]),
                        double.Parse(dict["Length"]),
                        bool.Parse(dict["InsectTreated"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.ReadyMixConcrete:
                    return new ReadyMixConcrete(double.Parse(dict["CementContent"]),
                        Enum.Parse<ReadyMixConcrete.Consistencies>(dict["Consistency"]),
                        Enum.Parse<ReadyMixConcrete.Types>(dict["TypeName"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.CrushedStone:
                    return new CrushedStone(double.Parse(dict["GrainSize"]),
                        bool.Parse(dict["Decorative"]),
                        double.Parse(dict["Weight"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.RockWool:
                    return new RockWool(double.Parse(dict["Thickness"]),
                        Enum.Parse<RockWool.Forms>(dict["Form"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                case AvaibleMaterials.MaterialTypes.Styrofoam:
                    return new Styrofoam(double.Parse(dict["Thickness"]),
                        bool.Parse(dict["StepResistant"]),
                        Enum.Parse<Styrofoam.BoardSizes>(dict["BoardSize"]),
                        double.Parse(dict["UnitPrice"]),
                        double.Parse(dict["VatPercent"])
                    );

                default:
                    throw new NotSupportedException($"Unsupported material type: {type}");
            }
        }

        #endregion
    }
}
