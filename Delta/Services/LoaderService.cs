using System.IO;
using Delta.Application.Extensions;
using Delta.Domain.Models.MTable;
using Delta.File.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Services;

public class LoaderService(IReaderService reader)
{
    public List<Table> XMLTables { get; set; } = new();
    public List<Table> CSVTables { get; set; } = new();
    public async Task OpenXML(IBrowserFile file) 
    {
        var xml = await reader.ReadAsync(file);
        XMLTables = xml.Value!.DeltaTables;
    }
    public async Task OpenCSVS(IReadOnlyList<IBrowserFile> files) 
    {
        foreach (var csv in files)
        {
            var table = await reader.ReadAsync(csv);
            CSVTables.Add(Table.BuildFromDataSet(table.Value!));
        }
    }

}
