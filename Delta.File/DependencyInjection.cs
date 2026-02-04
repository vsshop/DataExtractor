using System.Text;
using Delta.File.Interfaces;
using Delta.File.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.File;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddReaderServices()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddScoped<XLSXReaderService>();
            services.AddScoped<CSVReaderService>();
            services.AddScoped<XMLReaderService>();
            services.AddScoped<IReaderService, BaseReaderService>();

            return services;
        }
    }
}
