using System.Text.RegularExpressions;
using Delta.Application.Extensions;
using Delta.Application.Models;
using Delta.File.Extensions;
using Delta.File.Interfaces;
using Delta.Pipeline.Abstracts;
using Delta.Pipeline.Enums;
using Delta.Pipeline.Models;
using Microsoft.Extensions.Hosting;

namespace Delta.Application.Pipelines;

public class ValidatePipeline(IReaderService reader, IHostEnvironment environment) : BasePipeline
{
    public override string Title => "Валідація значень";
    public override string Icon => "marker";
    public override async Task Apply(PipelineContext context)
    {
        var file = Path.Combine(environment.ValidationRules, $"{context.Table.TitleInvariant}.xlsx");
        var info = new FileInfo(file);
        if (!info.Exists) return;

        var data = await reader.ReadAsync(file);

        foreach (var column in context.Table.Columns)
        {
            var rules = data.Value?.Tables[column.Title]?.Select<ValidateOperation>();
            if (rules is null) continue;

            var regexRules = new List<(Regex rx, ValidateOperation operation)>();
            foreach (var r in rules)
            {
                var current = r.Validate?.Trim();
                if (string.IsNullOrEmpty(current)) continue;

                if (current.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
                {
                    var pattern = current[6..].Trim();
                    if (string.IsNullOrWhiteSpace(pattern)) continue;

                    try
                    {
                        regexRules.Add((new Regex(pattern), r));
                    }
                    catch (ArgumentException) { }
                }
            }

            foreach (var row in context.Table.Rows)
            {
                var value = row.Cells[column.Index];
                foreach (var (rx, repl) in regexRules)
                {
                    if (!rx.IsMatch(value)) continue;
                    var level = (LevelCode) repl.Level;

                    if (level == LevelCode.Error)
                    {
                        row.Cells[column.Index] += "​";
                    }

                    if(level == LevelCode.Warning)
                    {
                        row.Cells[column.Index] += "⁠";
                    }
                    
                    context.Log.Add(new()
                    {
                        Row = row.Id,
                        Column = column.Id,
                        Message = repl.Message,
                        Level = level
                    });
                    break;
                }
            }
        }
    }
}
