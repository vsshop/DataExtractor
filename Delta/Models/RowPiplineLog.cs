using Delta.Domain.Enums;

namespace Delta.Models;

public class RowPiplineLog
{
    public Guid Column { get; set; }
    public Guid Row { get; set; }
    public LevelCode Level { get; set; }
    public string? Message { get; set; }
}
