using Delta.Domain.Models.MTable;

namespace Delta.Services;

public class UITableService(LoaderService loader)
{
    public List<Table> Tables { get; set; } = new();
    public async Task BuildTables()
    {
        Tables = new();
        foreach (var table in loader.CSVTables)
        {
            var scheme = loader.XMLTables.FirstOrDefault(t => t.TitleInvariant == table.TitleInvariant);
            if (scheme == null) continue;

            var clone = table.Clone();
            clone.Columns = scheme.Columns;
            Tables.Add(clone);
        }
    }
}
