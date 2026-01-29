namespace Delta.Domain.Models.MTable;

public class Row
{
    public Guid Id { get; set; }
    public List<string> Cells { get; set; } = new();

    public Row Clone() => new()
    {
        Id = Id,
        Cells = Cells.ToList()
    };
}
