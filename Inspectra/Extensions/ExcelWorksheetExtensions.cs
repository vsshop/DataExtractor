using Inspectra.Models;
using OfficeOpenXml;

namespace Inspectra.Extensions
{
    public static class ExcelWorksheetExtensions
    {
        public static string[][] Table(this ExcelWorksheet sheet)
        {
            var result = new List<string[]>();
            var tables = sheet.Tables.First();
            var range = tables.Range;
            for (int j = 0; j < range.Columns; j++)
            {
                var column = new List<string>();
                for (int i = 1; i < range.Rows; i++)
                {
                    column.Add(sheet.Cells[i + 1, j + 1].Text);
                }
                result.Add(column.ToArray());
            }
           
            return result.ToArray();
        }
    }
}
