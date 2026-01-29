using System.Data;
using Delta.Domain.Implements;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Excel.Interfaces;

public interface IReaderService
{
    Task<Result<DataSet>> ReadAsync(IBrowserFile file);
}
