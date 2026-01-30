using System.Data;
using Delta.Domain.Enums;
using Delta.Domain.Implements;

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

    public static Table BuildFromDataTable(DataTable table)
    {
        var columnCount = 0;
        var instanse = new Table();
        instanse.Title = table.TableName;
        foreach (DataColumn column in table.Columns)
        {
            var col = new Column()
            {
                Id = Guid.NewGuid(),
                Index = columnCount,
                Title = column.ColumnName,
            };
            instanse.Columns.Add(col);
        }

        foreach (DataRow row in table.Rows)
        {
            var cells = row.ItemArray.Select(i => i?.ToString() ?? string.Empty);
            var r = new Row()
            {
                Id = Guid.NewGuid(),
                Cells = cells.ToList()
            };
            instanse.Rows.Add(r);
        }

        return instanse;
    }
}
