namespace Delta.Domain.Models.MTable;

public class Column
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }

    public Column Clone() => new()
    { 
        Id = Id, 
        Title = Title, 
        Index = Index 
    };
}
