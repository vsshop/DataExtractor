using System.Data;
using Delta.Domain.Enums;

namespace Delta.Domain.Models.MTable;

public class Table
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public StateCode State { get; set; }
    public List<Row> Rows { get; set; } = new();
    public List<Column> Columns { get; set; } = new();
    public string TitleInvariant => Title.ToLowerInvariant();
    public Table Clone() => new()
    {
        Id = Id,
        Title = Title,
        State = State,
        Columns = Columns.Select(c => c.Clone()).ToList(),
        Rows = Rows.Select(r => r.Clone()).ToList(),
    };

    public Column AddColumn(string title)
    {
        var column = new Column()
        {
            Title = title,
            Id = Guid.NewGuid(),
            Index = Columns.Count
        };
        Columns.Add(column);

        foreach (var row in Rows)
        {
            row.Cells.Add(string.Empty);
        }
        return column;
    }


    public bool MoveColumn(Column column, int newIndex)
    {
        var currentIndex = Columns.IndexOf(column);
        if (currentIndex == -1) return false;

        if (newIndex < 0 || newIndex >= Columns.Count)
        {
            newIndex = Columns.Count - 1;
        }

        if (currentIndex == newIndex) return true;

        Columns.RemoveAt(currentIndex);
        Columns.Insert(newIndex, column);

        return true;
    }

    public bool MoveColumn(string title, int newIndex)
    {
        var column = Columns.FirstOrDefault(c => c.Title == title);
        if (column is null) return false;
        return MoveColumn(column, newIndex);
    }

    public bool RemoveColumn(Column column)
    {
        var index = Columns.IndexOf(column);
        if (index == -1) return false;

        Columns.RemoveAt(index);

        foreach (var row in Rows)
        {
            if (index < row.Cells.Count)
            {
                row.Cells.RemoveAt(index);
            }
        }

        for (int i = index; i < Columns.Count; i++)
        {
            Columns[i].Index = i;
        }

        return true;
    }

    public static Table BuildFromDataTable(DataTable table)
    {
        var columnCount = 0;
        var instanse = new Table();
        instanse.Id = Guid.NewGuid();
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
            columnCount++;
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

    public static Table BuildFromDataSet(DataSet data)
    {
        return BuildFromDataTable(data.Tables[0]);
    }
}
