using Delta.Application.Pipelines;
using Delta.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Application;

public static class DepndencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddHttpClient();
            services.AddScoped<ColumnRenamePipeline>();
            services.AddScoped<ReplacePipeline>();
            services.AddScoped<ColumnSortPipeline>();
            services.AddScoped<ValidatePipeline>();
            services.AddScoped<RelevantPipeline>();
            services.AddScoped<SplitAddressPipeline>();

            services.AddPipeline(builder =>
            {
                builder.Next<ColumnRenamePipeline>()
                       .Next<ColumnSortPipeline>()
                       .Next<ReplacePipeline>()
                       .Next<ValidatePipeline>()
                       .Next<RelevantPipeline>()
                       .Next<SplitAddressPipeline>();
            });

            return services;
        }
    }
}
