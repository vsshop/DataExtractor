using System.IO;
using System.Text;
using System.Xml.Linq;
using Delta.Domain.Models.MTable;
using Delta.Pipeline.Models;
using Delta.Pipeline.Services;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Delta.Services;

public class UIWriterService(PipelineService pipelines)
{
    public async Task WriteAsync(Table table, Encoding? encoding = null, char delimiter = ';')
    {
        var context = PipelineContext.Build(table);
        await pipelines.InvokeAsync(context);

        var dilog = new SaveFileDialog()
        {
            Title = "Сохранить файл",
            Filter = "Csv files (*.csv)|*.csv",
            FileName = $"{context.Table.Title}.csv"
        }; 

        if (dilog.ShowDialog() == true)
        {
            encoding ??= new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);

            string path = dilog.FileName;
            var csv = ToCsv(context.Table, delimiter);
            System.IO.File.WriteAllText(path, csv, encoding);
        }
    }

    public async Task WriteAsync(List<Table> tables, Encoding? encoding = null, char delimiter = ';')
    {
        var dialog = new CommonOpenFileDialog()
        {
            IsFolderPicker = true,
            Title = "Выберите папку"
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string folder = dialog.FileName;

            foreach (var table in tables)
            {
                var context = PipelineContext.Build(table);
                await pipelines.InvokeAsync(context);

                encoding ??= new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);

                string path = Path.Combine(folder, $"{context.Table.Title}.csv");
                var csv = ToCsv(context.Table, delimiter);
                System.IO.File.WriteAllText(path, csv, encoding);
            }
        }
    }

    public static string ToCsv(Table table, char delimiter = ';')
    {
        var sb = new StringBuilder();
        var columns = table.Columns.Select(c => Escape(c.Title ?? "", delimiter));
        sb.AppendLine(string.Join(delimiter, columns));

        foreach (var row in table.Rows)
        {
            var values = new string[table.Columns.Count];

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var idx = table.Columns[i].Index;

                string cell = (idx >= 0 && idx < row.Cells.Count)
                    ? (row.Cells[idx] ?? "")
                    : string.Empty;

                values[i] = Escape(cell, delimiter);
            }

            sb.AppendLine(string.Join(delimiter, values));
        }

        return sb.ToString();
    }

    private static string Escape(string value, char delimiter)
    {
        var mustQuote = value.Contains(delimiter)
                        || value.Contains('"')
                        || value.Contains('\n')
                        || value.Contains('\r');

        if (value.Contains('"'))
            value = value.Replace("\"", "\"\"");

        return mustQuote ? $"\"{value}\"" : value;
    }
}
