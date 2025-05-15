using Inspectra.Extensions;
using OfficeOpenXml;

namespace Inspectra.Models
{
    public class Column
    {
        public string Name { get; set; }
        public string[][] Data { get; set; }
        public Column(ExcelWorksheet sheet)
        {
            Name = sheet.Name;
            Data = sheet.Table();
        }

        public static explicit operator Column(ExcelWorksheet sheet) => new Column(sheet);
    }
}
