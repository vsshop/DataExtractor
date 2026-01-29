using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Telemetry.Builder;

public class WebHybridBuilder : IAsyncDisposable
{
    protected IHost? WebHost { get; set; }
    public IServiceCollection Services { get; }
    private WebHybridBuilder()
    {
        Services = new ServiceCollection();
        Services.AddWpfBlazorWebView();
        Services.AddBlazorWebViewDeveloperTools();
    }

    public static WebHybridBuilder CreateBuilder() => new WebHybridBuilder();

    public IHost Build()
    {
        WebHost = Host.CreateDefaultBuilder()
                      .ConfigureServices(Init)
                      .Build();

        WebHost.Start();
        return WebHost;
    }

    protected void Init(IServiceCollection services)
    {
        foreach (var service in Services) services.Add(service);
    }

    public async ValueTask DisposeAsync()
    {
        if (WebHost == null) return;

        await WebHost.StopAsync();
        WebHost.Dispose();
    }
}