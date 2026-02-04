using System.Data;
using System.Text;
using Delta.Domain.Implements;
using ExcelDataReader;

namespace Delta.File.Services;

public class CSVReaderService
{
    static Encoding DetectEncoding(byte[] bytes)
    {
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            try
            {
                _ = new UTF8Encoding(false, true).GetString(bytes, 3, bytes.Length - 3);
                return new UTF8Encoding(false, true);
            }
            catch
            {
                return Encoding.GetEncoding(1252);
            }
        }
        return Encoding.GetEncoding(1252);
    }

    public async Task<Result<DataSet>> ReadAsync(byte[] bytes)
    {
        var text = DetectEncoding(bytes).GetString(bytes);
        var normalized = new UTF8Encoding(false).GetBytes(text);

        var config = new ExcelReaderConfiguration()
        {
            FallbackEncoding = Encoding.UTF8,
            AutodetectSeparators = [';'],
            AnalyzeInitialCsvRows = 50
        };

        using var stream = new MemoryStream(normalized);
        using var reader = ExcelReaderFactory.CreateCsvReader(stream, config);

        var data = reader.AsDataSet();
        return Result<DataSet>.Ok(data);
    }

    public async Task<Result<DataSet>> ReadAndSetNameAsync(string name, byte[] bytes)
    {
        var result = await ReadAsync(bytes);
        var dataSet = result.Value!;
        dataSet.Tables[0].TableName = name;
        return Result<DataSet>.Ok(dataSet);
    }
}
