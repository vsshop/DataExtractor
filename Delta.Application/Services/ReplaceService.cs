using Delta.Application.Extensions;
using Delta.Application.Models;
using Delta.Excel.Extensions;
using Delta.Excel.Services;
using Microsoft.Extensions.Hosting;

namespace Delta.Application.Services;

public class ReplaceService(IHostEnvironment environment, XLSXReaderService reader)
{
    public async Task<List<ReplaceOperation>> Replace()
    {
        var files = Directory.GetFiles(environment.ReplaceRules);

        var file = files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == "First Table");
        if (file == null) return new();

        var bytes = File.ReadAllBytes(file);
        var result = await reader.ReadAsync(bytes);
        if (!result) return new();

        return result.Value!.Tables[0].Select<ReplaceOperation>();
    }
}
