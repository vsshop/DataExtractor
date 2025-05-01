using Inspectra.Controls;
using Inspectra.Hosts;
using Inspectra.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inspectra.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddForms(this IServiceCollection services, Assembly? assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            var types = assembly!.GetTypes();
            var windowTypes = types.Where(t =>
            {
                return t.IsClass
                && !t.IsAbstract
                && t.IsSubclassOf(typeof(Form));
            });

            foreach (var windowType in windowTypes)
            {
                services!.AddTransient(windowType);
            }
        }

        public static void AddServer(this IServiceCollection services, string root = "Resources", int port = 4200)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, root, "Browser");
            services.AddSingleton(provider => new HostServer(path, port));
            services.AddHostedService(provider => provider.GetRequiredService<HostServer>());
        }

        public static void AddAngular(this IServiceCollection services)
        {
            services.AddTransient<AngularView>();
            services.AddSingleton<AngularService>();
        }
    }
}
