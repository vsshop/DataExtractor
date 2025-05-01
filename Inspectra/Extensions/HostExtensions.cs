using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inspectra.Extensions
{
    public static class HostExtensions
    {
        public static void Show<T>(this IHost host) where T : Form
        {
            var form = host.Services.GetService<T>()!;
            Application.Run(form);
        }
    }
}
