namespace Delta.Application.Models;

public class RenameOperation
{
    public string Column { get; set; } = string.Empty;
    public string? Rename { get; set; }
}
