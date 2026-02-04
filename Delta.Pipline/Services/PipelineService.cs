using Delta.Pipeline.Abstracts;
using Delta.Pipeline.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Pipeline.Services;

public class PipelineService(IServiceProvider provider)
{
    public List<BasePipeline> Pipelines { get; private set; } = new();
    public PipelineService Next<T>() where T : BasePipeline
    {
        Pipelines.Add(ActivatorUtilities.CreateInstance<T>(provider));
        return this;
    }

    public async Task InvokeAsync(PipelineContext context, int steps = int.MaxValue, CancellationToken token = default)
    {
        int current = 0;
        foreach (var pipeline in Pipelines)
        {
            if(token.IsCancellationRequested) return;
            if(current > steps) return;

            context.Log.Clear();
            if (pipeline.Enabled)
            {
                await pipeline.Apply(context);
            }

            current++;
        }
    }
}
