using System.IO;
using Delta.Application.Extensions;
using Delta.Domain.Models.MTable;
using Delta.File.Interfaces;

namespace Delta.Services;

public class PreloadService(IReaderService reader)
{
    public List<Table> XMLTables = new();
    public List<Table> CSVTables = new();
    public List<Table> Tables = new();
    const string ROOT = "C:\\Users\\NEKO\\Documents\\DataExtractor";
    string XMLPath = Path.Combine(ROOT, "Index.xml");
    public async Task LoadAsync()
    {
        var xml = await reader.ReadAsync(XMLPath);
        XMLTables = xml.Value!.DeltaTables;

        var csvs = Directory.GetFiles(ROOT, "*.csv");
        foreach (var csv in csvs)
        {
            var table = await reader.ReadAsync(csv);
            CSVTables.Add(Table.BuildFromDataSet(table.Value!));
        }

        foreach (var table in CSVTables)
        {
            var scheme = XMLTables.FirstOrDefault(t => t.TitleInvariant == table.TitleInvariant);
            if (scheme == null) continue;

            var clone = table.Clone();
            clone.Columns = scheme.Columns;
            Tables.Add(clone);
        }
    }
}
