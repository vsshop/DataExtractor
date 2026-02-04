using System.Data;
using System.Xml.Linq;
using Delta.Domain.Implements;

namespace Delta.File.Services;

public class XMLReaderService
{
    public Task<Result<DataSet>> ReadAsync(byte[] bytes)
    {
        try
        {
            using var stream = new MemoryStream(bytes);
            var doc = XDocument.Load(stream);

            var dataSet = new DataSet();
            var tables = doc.Descendants("Table");

            foreach (var table in Tables(tables))
            {
                if (dataSet.Tables.Contains(table.TableName)) continue;

                dataSet.Tables.Add(table);
            }

            return Task.FromResult(Result<DataSet>.Ok(dataSet));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result<DataSet>.Conflict(ex.Message));
        }
    }

    private IEnumerable<DataTable> Tables(IEnumerable<XElement> tables)
    {
        foreach (var table in tables)
        {
            var value = table.Element("Name")?.Value;
            if (string.IsNullOrWhiteSpace(value)) continue;

            var name = value?.Trim().ToLowerInvariant();
            var data = new DataTable(name);

            var columns = table.Descendants("VariableColumn");
            foreach (var column in Columns(columns))
            {
                data.Columns.Add(column, typeof(string));
            }

            yield return data;
        }
    }

    private IEnumerable<string> Columns(IEnumerable<XElement> columns)
    {
        foreach (var column in columns)
        {
            var name = column.Element("Name")?.Value;
            if (string.IsNullOrWhiteSpace(name)) continue;

            yield return name.Trim();
        }
    }
}
