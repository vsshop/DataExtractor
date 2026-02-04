using System.Data;
using Delta.Domain.Implements;
using Delta.File.Extensions;
using Delta.File.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.File.Services;

public class BaseReaderService(XLSXReaderService xlsx, CSVReaderService csv, XMLReaderService xml) : IReaderService
{
    public async Task<Result<DataSet>> ReadAsync(IBrowserFile file)
    {
        await using var upload = file.OpenReadStream(long.MaxValue);
        using var ms = new MemoryStream();
        await upload.CopyToAsync(ms);
        var bytes = ms.ToArray();

        return file.Extension switch
        {
            ".csv" => await csv.ReadAndSetNameAsync(file.DisplayName, bytes),
            ".xlsx" or ".xls" => await xlsx.ReadAsync(bytes),
            ".xml" => await xml.ReadAsync(bytes),
            _ => Result<DataSet>.Validation("")
        };
    }

    public async Task<Result<DataSet>> ReadAsync(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            return Result<DataSet>.NotFound("File not found");
        }

        var bytes = await System.IO.File.ReadAllBytesAsync(path);
        var file = new FileInfo(path);
        return file.Extension switch
        {
            ".csv" => await csv.ReadAndSetNameAsync(file.DisplayName, bytes),
            ".xlsx" or ".xls" => await xlsx.ReadAsync(bytes),
            ".xml" => await xml.ReadAsync(bytes),
            _ => Result<DataSet>.Validation("")
        };
    }
}