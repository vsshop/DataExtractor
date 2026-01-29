namespace Delta.Models;

public sealed class MetaPipeline
{
    private int step = 0;
    private int next = 0;
    private int nextId = 0;
    public event Action? OnStepChange;
    private readonly Dictionary<string, int> ids = new();

    public int Steps => next;
    public int Step 
    { 
        get => step; 
        set  { step = value; OnStepChange?.Invoke(); }  
    }
    public int Register()
    {
        var key = $"MetaPipeline{nextId++}";
        if (ids.TryGetValue(key, out var idx)) return idx;

        idx = next++;
        ids[key] = idx;
        return idx;
    }
}
