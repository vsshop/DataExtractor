using Delta.Domain.Models.MTable;

namespace Delta.Pipeline.Models;

public class PipelineContext
{
    public required Table Table { get; set; }
    public List<PipelineLog> Log { get; } = new();

    public static PipelineContext Build(Table table) => new()
    {
        Table = table.Clone()
    };

    public async Task<bool> TryApply(int index, Func<PipelineContext, Task> apply)
    {
        Log.Clear();
        await apply(this);

        return true;
    }
}
