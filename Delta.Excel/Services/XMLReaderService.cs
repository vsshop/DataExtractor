using System.Data;
using System.Xml.Linq;
using Delta.Domain.Implements;

namespace Delta.Excel.Services;

public class XMLReaderService
{
    public Task<Result<DataSet>> ReadAsync(byte[] bytes)
    {
        try
        {
            using var stream = new MemoryStream(bytes);
            var doc = XDocument.Load(stream);

            var ds = new DataSet();
            var tables = doc.Descendants("Table");

            foreach (var t in tables)
            {
                var tableName = (string?)t.Element("Name");
                if (string.IsNullOrWhiteSpace(tableName))
                    continue;

                tableName = tableName.Trim();
                var dt = new DataTable(tableName);

                var colNames = t.Descendants("VariableColumn")
                                .Select(vc => (string?)vc.Element("Name"))
                                .Where(n => !string.IsNullOrWhiteSpace(n))
                                .Select(n => n!.Trim())
                                .Distinct(StringComparer.Ordinal);

                foreach (var colNameRaw in colNames)
                {
                    var colName = MakeUniqueColumnName(dt, colNameRaw);
                    dt.Columns.Add(colName, typeof(string));
                }

                if (ds.Tables.Contains(dt.TableName))
                {
                    dt.TableName = MakeUniqueTableName(ds, dt.TableName);
                }

                ds.Tables.Add(dt);
            }

            return Task.FromResult(Result<DataSet>.Ok(ds));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result<DataSet>.Conflict(ex.Message));
        }
    }

    private static string MakeUniqueTableName(DataSet ds, string baseName)
    {
        var name = baseName;
        var i = 1;
        while (ds.Tables.Contains(name))
            name = $"{baseName}_{i++}";
        return name;
    }

    private static string MakeUniqueColumnName(DataTable dt, string baseName)
    {
        var name = baseName;
        var i = 1;
        while (dt.Columns.Contains(name))
            name = $"{baseName}_{i++}";
        return name;
    }
}
