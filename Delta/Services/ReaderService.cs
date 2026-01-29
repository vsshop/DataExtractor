using System.Data;
using Delta.Excel.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Services;

public class ReaderService(IReaderService reader)
{
    public async Task<DataSet> Read(IBrowserFile file)
    {
        var data = await reader.ReadAsync(file);
        if (!data) throw new Exception();

        return data.Value!;
    }
}
