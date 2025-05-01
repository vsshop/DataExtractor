using Inspectra.Controls;
using Inspectra.Extensions;
using Inspectra.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inspectra
{
    public class App
    {
        private IHost? host;
        private readonly HostApplicationBuilder builder;

        public IServiceCollection Services { get => builder.Services; }

        private App(string[]? args = null)
        {
            builder = Host.CreateApplicationBuilder(args ?? Array.Empty<string>());
        }

        public static App Create(string[]? args = null)
        {
            ApplicationConfiguration.Initialize();

            var app = new App(args);
            app.builder.Services.AddServer();
            app.builder.Services.AddForms();
            return app;
        }

        public async void Start<T>() where T : Form
        {
            builder.Services.AddAngular();
            host = builder.Build();
            await host.StartAsync();
            host.Show<T>();
        }
    }
}
