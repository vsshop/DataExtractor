using Delta.Application.Extensions;
using Delta.Application.Models;
using Delta.Domain.Models.MTable;
using Delta.File.Extensions;
using Delta.File.Interfaces;
using Delta.Pipeline.Abstracts;
using Delta.Pipeline.Enums;
using Delta.Pipeline.Models;
using Microsoft.Extensions.Hosting;

namespace Delta.Application.Pipelines;

public class ColumnSortPipeline(IReaderService reader, IHostEnvironment environment) : BasePipeline
{
    public override string Title =>  "Сортування колонок";
    public override string Icon => "list";
    private string File => Path.Combine(environment.Settings, "column-sort.xlsx");
    public override async Task Apply(PipelineContext context)
    {
        var info = await reader.ReadAsync(File);
        if (!info) return;

        var key = context.Table.TitleInvariant;
        var operations = info.Value!.Tables[key]?.Select<RenameOperation>();
        if (operations is null) return;


        var byTitle = context.Table.Columns.ToDictionary(c => c.Title);
        var reordered = new List<Column>(operations.Count);

        for (int i = 0; i < operations.Count; i++)
        {
            if (!byTitle.TryGetValue(operations[i].Column, out var column))
                continue;

            if (column.Index != i)
            {

                context.Log.Add(new()
                {
                    Column = column.Id,
                    Level = LevelCode.Info,
                    Message = "Rename column"
                });
                foreach (var row in context.Table.Rows)
                {
                    context.Log.Add(new()
                    {
                        Row = row.Id,
                        Column = column.Id,
                        Level = LevelCode.Info,
                        Message = "Sort column"
                    });
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
}
