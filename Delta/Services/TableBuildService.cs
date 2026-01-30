using System.Data;
using Delta.Application.Extensions;
using Delta.Domain.Models.MTable;
using Delta.Excel.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Services;

public class TableBuildService(IReaderService reader)
{
    public async Task<List<Table>> Build(DataSet document, IReadOnlyList<IBrowserFile> files)
    {
        var tables = document.DeltaTables;

        foreach (var file in files)
        {
            var data = await reader.ReadAsync(file);
            if (!data) return new();

            var csvs = data.Value!.DeltaTables;

            foreach(var csv in csvs)
            {
                var scheme = tables.FirstOrDefault(t => t.Title == csv.Title);
                if (scheme == null) continue;

                csv.Columns = scheme.Columns;
            }

            return csvs;
        }

        return new();
    }
}
