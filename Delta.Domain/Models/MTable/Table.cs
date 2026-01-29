using Delta.Domain.Enums;

namespace Delta.Domain.Models.MTable;

public class Table
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public StateCode State { get; set; }
    public List<Row> Rows { get; set; } = new();
    public List<Column> Columns { get; set; } = new();
    public Table Clone() => new()
    {
        Id = Id,
        Title = Title,
        State = State,
        Columns = Columns.Select(c => c.Clone()).ToList(),
        Rows = Rows.Select(r => r.Clone()).ToList(),
    };
}
