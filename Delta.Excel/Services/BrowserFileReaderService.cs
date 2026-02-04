using System.Data;
using Delta.Domain.Implements;
using Delta.Excel.Extensions;
using Delta.Excel.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Excel.Services;

public class BrowserFileReaderService(XLSXReaderService xlsx, CSVReaderService csv, XMLReaderService xml) : IReaderService
{
    public async Task<Result<DataSet>> ReadAsync(IBrowserFile file)
    {
        await using var upload = file.OpenReadStream(long.MaxValue);
        using var ms = new MemoryStream();
        await upload.CopyToAsync(ms);
        var bytes = ms.ToArray();

        return file.Extension switch
        {
            ".csv" => await csv.ReadSetNameAsync(file.DisplayName, bytes),
            ".xlsx" or ".xls" => await xlsx.ReadAsync(bytes),
            ".xml" => await xml.ReadAsync(bytes),
            _ => Result<DataSet>.Validation("")
        };
    }
}
