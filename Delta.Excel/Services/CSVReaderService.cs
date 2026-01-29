using System.Data;
using System.Text;
using Delta.Domain.Implements;
using ExcelDataReader;

namespace Delta.Excel.Services;

public class CSVReaderService
{
    private static Encoding[] Encodes =>
    [
        new UTF8Encoding(false, true),
        Encoding.GetEncoding("iso-8859-1"),
        Encoding.GetEncoding(1251),
        Encoding.GetEncoding(1252),
        Encoding.GetEncoding(1250)
    ];

    public async Task<Result<DataSet>> ReadAsync(byte[] bytes)
    {
        Exception? exception = null;
        foreach (var encode in Encodes)
        {
            try
            {
                using var stream = new MemoryStream(bytes);
                var config = new ExcelReaderConfiguration()
                {
                    FallbackEncoding = encode
                };
                using var reader = ExcelReaderFactory.CreateCsvReader(stream, config);

                var data = reader.AsDataSet();
                return Result<DataSet>.Ok(data);
            }
            catch (Exception ex) { exception = ex; }
        }
        return Result<DataSet>.Conflict(exception!.Message);
    }
}
