using Delta.Domain.Enums;
using Delta.Domain.Models.MTable;

namespace Delta.Services;
public class DataService
{
    static Table table = new Table()
    {
        Id = Guid.Parse("e39d3625-029a-48fc-a316-e1214337b235"),
        Title = "First Table",
        State = StateCode.Validate,
        Columns = new()
        {
            new Column()
            {
                Id = Guid.Parse("42d9f4c8-b3b5-44af-9ad6-b0eaa55a0acb"),
                Title = "Id",
                Index = 0
            },
            new Column()
            {
                Id = Guid.Parse("ece9715a-f554-43d8-a55e-2d6e8a8953cf"),
                Title = "Name",
                Index = 1
            },
            new Column()
            {
                Id = Guid.Parse("fd462a28-6735-4c38-8263-57cee1ec1708"),
                Title = "Salary",
                Index = 2
            },
        },
        Rows = new List<Row>()
        {
            new Row()
            {
                Id = Guid.Parse("dc79a2a8-87c3-4c99-bb3a-505b1709bff7"),
                Cells = new() {"1", "Jon", "5000"}
            },
            new Row()
            {
                Id = Guid.Parse("bfd8437d-9fea-4861-8574-b3130b4cf7bc"),
                Cells = new() {"2", "Lili", "12000"}
            },
            new Row()
            {
                Id = Guid.Parse("dece7e53-7e78-41cd-8b6f-e5967c9eabad"),
                Cells = new() {"3", "Esma", "10000"}
            },
            new Row()
            {
                Id = Guid.Parse("9b70eeb6-a7b5-4afc-bf94-ddcf42774bd5"),
                Cells = new() {"4", "Arnold", "51000"}
            },
            new Row()
            {
                Id = Guid.Parse("6e5d9995-b3c0-4071-8cbb-1244e3bbf699"),
                Cells = new() {"5", "Kevin", "3000"}
            }
        }
    };


    public Table Table => table;
}
