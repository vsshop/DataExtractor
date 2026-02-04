using Delta.Pipeline.Models;

namespace Delta.Pipeline.Abstracts;

public abstract class BasePipeline
{
    public bool Enabled { get; set; } = true;
    public virtual string Icon => "pen-to-square";
    public virtual string Title => "Pipeline";
    public virtual void Switch() => Enabled = !Enabled;
    public abstract Task Apply(PipelineContext context);
}
