using Delta.Pipeline.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Pipeline;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPipeline(Action<PipelineService> builder)
        {
            services.AddScoped(provider =>
            {
                var context = new PipelineService(provider);
                builder(context);
                return context;
            });

            return services;
        }
    }
}
