using System.Data;
using Delta.Domain.Models.MTable;

namespace Delta.Application.Extensions;

public static class DataSetExtensions
{
    extension(DataSet dataSet)
    {
        public List<Table> DeltaTables
        {
            get => dataSet.Tables.Cast<DataTable>().Select(Table.BuildFromDataTable).ToList();
        }
    }
}
