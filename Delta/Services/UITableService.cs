using System.Text.RegularExpressions;
using Delta.Application.Models;
using Delta.Application.Services;
using Delta.Domain.Enums;
using Delta.Domain.Models.MTable;
using Delta.Models;

namespace Delta.Services;

public class UITableService(RenameService renameService, SortingService sortingService, ReplaceService replaceService, ValidateService validateService)
{
    public async Task ApplyRename(TablePipelineContext context)
    {
        var list = await renameService.Rename();
        context.Table.Columns.ForEach(c =>
        {
            var rule = list.FirstOrDefault(r => r.Column == c.Title);
            if (rule is not null && rule.Rename is not null)
            {
                c.Title = rule.Rename;
                context.ColumnsLog.Add(c.Id);
            }
        });
    }

    public async Task ApplySorting(TablePipelineContext context)
    {
        var sort = await sortingService.Sort();

        var byTitle = context.Table.Columns.ToDictionary(c => c.Title);
        var reordered = new List<Column>(sort.Count);

        for (int i = 0; i < sort.Count; i++)
        {
            if (!byTitle.TryGetValue(sort[i].Column, out var column))
                continue;

            if (column.Index != i)
            {

                context.ColumnsLog.Add(column.Id);
                foreach (var row in context.Table.Rows)
                {
                    var log = new RowPiplineLog()
                    {
                        Row = row.Id,
                        Column = column.Id,
                        Message = "sort column",
                        Level = LevelCode.Info
                    };
                    context.CellsLog.Add(log);
                }
            }

            reordered.Add(new Column()
            {
                Id = column.Id,
                Title = column.Title,
                Index = column.Index
            });
        }

        context.Table.Columns = reordered;
    }

    public async Task ApplyValidate(TablePipelineContext context)
    {
        var validation = await validateService.Validate();
        var column = context.Table.Columns.First();
        var regexRules = new List<(Regex rx, ValidateOperation operation)>();
        foreach (var r in validation)
        {
            var current = r.Validate?.Trim();
            if (string.IsNullOrEmpty(current)) continue;

            if (current.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
            {
                var pattern = current[6..].Trim();
                if (string.IsNullOrWhiteSpace(pattern)) continue;

                try
                {
                    var rx = GetRegexCached(pattern);
                    regexRules.Add((rx, r));
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

                context.CellsLog.Add(new RowPiplineLog()
                {
                    Column = column.Id,
                    Row = row.Id,
                    Level = (LevelCode) repl.Level,
                    Message = repl.Message
                });
                break;
            }
        }
    }

    public async Task ApplyReplace(TablePipelineContext context)
    {
        var replace = await replaceService.Replace();
        var column = context.Table.Columns.First();

        var exact = new Dictionary<string, string>(StringComparer.Ordinal);
        var regexRules = new List<(Regex rx, string replace)>();

        foreach (var r in replace)
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
                    var rx = GetRegexCached(pattern);
                    regexRules.Add((rx, repl));
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
                    context.CellsLog.Add(new RowPiplineLog()
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
                    context.CellsLog.Add(new RowPiplineLog()
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

    private static readonly Dictionary<string, Regex> _regexCache = new(StringComparer.Ordinal);

    private static Regex GetRegexCached(string pattern)
    {
        if (_regexCache.TryGetValue(pattern, out var rx))
            return rx;

        rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
        _regexCache[pattern] = rx;
        return rx;
    }
}
