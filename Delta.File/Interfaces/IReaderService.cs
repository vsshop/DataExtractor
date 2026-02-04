using System.Data;
using Delta.Domain.Implements;
using Microsoft.AspNetCore.Components.Forms;

namespace Delta.File.Interfaces;

public interface IReaderService
{
    Task<Result<DataSet>> ReadAsync(IBrowserFile file);
    Task<Result<DataSet>> ReadAsync(string file);
}
