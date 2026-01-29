using Delta.Domain.Models.MTable;

namespace Delta.Models;

public sealed class TablePipelineContext
{
    public int Limit { get; set; }
    public int Applied { get; set; }
    public required Table Table { get; set; }
    public HashSet<Guid> ColumnsLog { get; } = new();
    public List<RowPiplineLog> CellsLog { get; } = new();

    public static TablePipelineContext Build(Table table, int limit = 0) => new()
    {
        Table = table.Clone(),
        Limit = limit
    };

    public bool TryApply(int index, Func<TablePipelineContext, Task> apply)
    {
        if (Applied != index) return false;
        if (index >= Limit) return false;

        CellsLog.Clear();
        ColumnsLog.Clear();

        apply(this);
        Applied++;
        return true;
    }
}