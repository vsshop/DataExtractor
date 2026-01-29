using System.Data;
using Delta.Domain.Implements;
using Delta.Excel.Extensions;
using Delta.Excel.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Components.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Delta.Excel.Services;

public class BrowserFileReaderService(XLSXReaderService xlsx, CSVReaderService csv) : IReaderService
{
    public async Task<Result<DataSet>> ReadAsync(IBrowserFile file)
    {
        await using var upload = file.OpenReadStream(long.MaxValue);
        using var ms = new MemoryStream();
        await upload.CopyToAsync(ms);
        var bytes = ms.ToArray();

        return file.Extension switch
        {
            ".xlsx" or ".xls" => await xlsx.ReadAsync(bytes),
            ".csv" => await csv.ReadAsync(bytes),
            _ => Result<DataSet>.Validation("")
        };
    }
}
