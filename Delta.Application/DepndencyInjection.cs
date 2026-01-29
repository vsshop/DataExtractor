using Delta.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Application;

public static class DepndencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddScoped<RenameService>();
            services.AddScoped<SortingService>();
            services.AddScoped<ReplaceService>();
            services.AddScoped<ValidateService>();

            return services;
        }
    }
}
