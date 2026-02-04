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

public class ReplacePipeline(IReaderService reader, IHostEnvironment environment) : BasePipeline
{
    public override string Title => "Заміна значень";
    public override string Icon => "spell-check";
    public override async Task Apply(PipelineContext context)
    {
        var file = Path.Combine(environment.ReplaceRules, $"{context.Table.TitleInvariant}.xlsx");
        var info = new FileInfo(file);
        if (!info.Exists) return;

        var data = await reader.ReadAsync(file);

        foreach (var column in context.Table.Columns)
        {
            var rules = data.Value?.Tables[column.Title]?.Select<ReplaceOperation>();
            if(rules is null) continue;

            var exact = new Dictionary<string, string>(StringComparer.Ordinal);
            var regexRules = new List<(Regex rx, string replace)>();

            foreach (var r in rules)
            {
                var current = r.Current?.Trim();
                if (string.IsNullOrEmpty(current)) continue;

                var repl = r.Replace ?? "";

                if (current.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
                {
                    var pattern = current[6..].Trim();
                    if (string.IsNullOrWhiteSpace(pattern)) continue;

                    try
                    {
                        regexRules.Add((new Regex(pattern), repl));
                    }
                    catch (ArgumentException) { }
                }
                else
                {
                    exact[current] = repl;
                }
            }


            foreach (var row in context.Table.Rows)
            {
                if (column.Index >= row.Cells.Count) continue;

                var value = row.Cells[column.Index];

                if (exact.TryGetValue(value, out var exactReplace))
                {
                    if (!string.Equals(value, exactReplace, StringComparison.Ordinal))
                    {
                        row.Cells[column.Index] = exactReplace;
                        context.Log.Add(new()
                        {
                            Column = column.Id,
                            Row = row.Id,
                            Level = LevelCode.Info,
                            Message = "Value was replaced"
                        });
                    }
                    continue;
                }

                foreach (var (rx, repl) in regexRules)
                {
                    if (!rx.IsMatch(value)) continue;

                    var newValue = rx.Replace(value, repl);
                    if (!string.Equals(value, newValue, StringComparison.Ordinal))
                    {
                        row.Cells[column.Index] = newValue;
                        context.Log.Add(new()
                        {
                            Column = column.Id,
                            Row = row.Id,
                            Level = LevelCode.Warning,
                            Message = "Value was replaced by regex"
                        });
                    }
                    break;
                }
            }
        }

    }
}
