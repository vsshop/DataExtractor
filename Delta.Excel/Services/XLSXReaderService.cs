using System.Data;
using Delta.Domain.Implements;
using ExcelDataReader;

namespace Delta.Excel.Services;

public class XLSXReaderService
{
    static ExcelDataSetConfiguration Configuration => new()
    {
        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
        {
            UseHeaderRow = true
        }
    };

    public async Task<Result<DataSet>> ReadAsync(byte[] bytes)
    {
        Exception? exception = null;
        try
        {
            using var s = new MemoryStream(bytes);
            using var reader = ExcelReaderFactory.CreateReader(s);
            var data = reader.AsDataSet(Configuration);
            return Result<DataSet>.Ok(data);
        }
        catch (Exception ex) { exception = ex; }
        return Result<DataSet>.Conflict(exception!.Message);
    }
}
