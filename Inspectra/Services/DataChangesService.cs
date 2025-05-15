using Inspectra.Attributes;
using Inspectra.Models;
using OfficeOpenXml;

namespace Inspectra.Services
{
    public class DataChangesService
    {
        static DataChangesService()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Excel");
        }

        [Angular]
        public Data[] DataAngular()
        {
            return Data().ToArray();
        }

        public IEnumerable<Data> Data()
        {
            foreach (var file in Files)
            {
                if (file.Contains("~$")) continue;

                using var excel = new ExcelPackage(file);
                var columns = Columns(excel).ToArray();
                var name = Path.GetFileNameWithoutExtension(file);
                yield return new Data
                {
                    Columns = columns,
                    Name = name
                };
            }
        }

        public IEnumerable<Column> Columns(ExcelPackage excel)
        {
            foreach (var sheet in excel.Workbook.Worksheets)
            {
                yield return (Column) sheet;
            }
        }

        public string[] Files
        {
            get
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(directory, "Data");
                return Directory.GetFiles(path, "*.xlsx");
            }
        }
    }
}
