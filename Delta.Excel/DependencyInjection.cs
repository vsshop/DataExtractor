using System.Text;
using Delta.Excel.Interfaces;
using Delta.Excel.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Excel;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddExcelServices()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddScoped<XLSXReaderService>();
            services.AddScoped<CSVReaderService>();
            services.AddScoped<IReaderService, BrowserFileReaderService>();

            return services;
        }
    }
}
