using Delta.Application.Extensions;
using Delta.Application.Models;
using Delta.File.Extensions;
using Delta.File.Interfaces;
using Delta.Pipeline.Abstracts;
using Delta.Pipeline.Enums;
using Delta.Pipeline.Models;
using Microsoft.Extensions.Hosting;

namespace Delta.Application.Pipelines;

public class ColumnRenamePipeline(IReaderService reader, IHostEnvironment environment) : BasePipeline
{
    public override string Title => "Перейменування колонок";
    public override string Icon => "spell-check";
    private string File => Path.Combine(environment.Settings, "column-rename.xlsx");
    public override async Task Apply(PipelineContext context)
    {
        var info = await reader.ReadAsync(File);
        if(!info) return;

        var key = context.Table.TitleInvariant;
        var operations = info.Value!.Tables[key]?.Select<RenameOperation>();
        if (operations is null) return;

        context.Table.Columns.ForEach(c =>
        {
            var rule = operations.FirstOrDefault(r => r.Column == c.Title);
            if (rule is not null && rule.Rename is not null)
            {
                c.Title = rule.Rename;
                context.Log.Add(new()
                {
                    Column = c.Id,
                    Row = default,
                    Level = LevelCode.Info,
                    Message = "Rename column"
                });
            }
        });
    }
}
