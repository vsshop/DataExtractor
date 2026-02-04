namespace Delta.Pipeline.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class PipelineAssetsAttribute(string title, string icon) : Attribute
{
    public string Icon => icon;
    public string Title => title;
}
