using System.Data;
using System.Reflection.Emit;
using Delta.Application.Extensions;
using Delta.Application.Models;
using Delta.Domain.Models.MTable;
using Delta.Excel.Extensions;
using Delta.Excel.Services;
using Microsoft.Extensions.Hosting;

namespace Delta.Application.Services;

public class RenameService(IHostEnvironment environment, XLSXReaderService reader)
{
    public async Task<List<RenameOperation>> Rename()
    {
        var files = Directory.GetFiles(environment.ReplaceRules);

        var file = files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == "Test");
        if (file == null) return new();

        var bytes = File.ReadAllBytes(file);
        var result = await reader.ReadAsync(bytes);
        if (!result) return new();

        return result.Value!.Tables[0].Select<RenameOperation>();
    }
}
