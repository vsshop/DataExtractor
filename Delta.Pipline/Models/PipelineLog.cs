using Delta.Pipeline.Enums;

namespace Delta.Pipeline.Models;

public class PipelineLog
{
    public Guid Row { get; set; }
    public Guid Column { get; set; }
    public LevelCode Level { get; set; }
    public string? Message { get; set; }
}
