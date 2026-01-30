using System.Data;
using Delta.Domain.Models.MTable;

namespace Delta.Application.Extensions;

public static class DataTableExtensions
{
    public static T Map<T>(DataRow row) where T : new()
    {
        var obj = new T();
        var props = typeof(T).GetProperties();

        foreach (var p in props)
        {
            if (!row.Table.Columns.Contains(p.Name)) continue;
            var val = row[p.Name];
            if (val == DBNull.Value) continue;

            p.SetValue(obj, Convert.ChangeType(val, p.PropertyType));
        }

        return obj;
    }

    extension(DataTable table)
    {
        public List<T> Select<T>() where T : new()
        {
            return table.AsEnumerable()
                        .Select(Map<T>)
                        .ToList();
        }
    }
}
