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

public class RelevantPipeline : BasePipeline
{
    public override string Title =>  "Релевантні значення";
    public override string Icon => "pen-to-square";
    public override async Task Apply(PipelineContext context)
    {
        var columns = context.Table.Columns.Select(c => c.Title);
        if(!CheckForRelevantColumns(columns)) return;

        var rows = new List<Row>();
        var personNumber = context.Table.Columns.First(c => c.Title == "Pers_Nr");
        var date = context.Table.Columns.First(c => c.Title == "Anm_Jahr");
        var month = context.Table.Columns.First(c => c.Title == "Abr_Mon");
        var groups = context.Table.Rows.GroupBy(r => r.Cells[personNumber.Index]);

        foreach( var group in groups)
        {
            var max = group.MaxBy(row => RowCalculateRelevant(row, date, month));
            if (max is null)
            {
                rows.AddRange(group);
                continue;
            }

            context.Log.Add(new()
            {
                Row = max.Id,
                Level = LevelCode.Info,
                Message = $"Last Relevant Value for {group.Key}"
            });
            rows.Add(max);
        }

        context.Table.Rows = rows;
    }

    private bool CheckForRelevantColumns(IEnumerable<string> columns)
    {
        return columns.Contains("Pers_Nr") && columns.Contains("Anm_Jahr") && columns.Contains("Abr_Mon");
    }

    private int RowCalculateRelevant(Row row, Column year, Column month)
    {
        return Convert.ToInt32(row.Cells[year.Index]) + Convert.ToInt32(row.Cells[month.Index]);
    }
}
