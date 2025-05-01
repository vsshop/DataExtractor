using EmbedIO;
using Microsoft.Extensions.Hosting;

namespace Inspectra.Hosts
{
    public class HostServer(string root, int port) : IHostedService
    {
        private Task? task;
        public string URL { get => $"http://localhost:{port}"; }

        private readonly WebServer _server = new WebServer(options => options
                .WithUrlPrefix($"http://localhost:{port}")
                .WithMode(HttpListenerMode.EmbedIO))
                .WithStaticFolder("/", root, true);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            task = _server.RunAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(task != null) await task;
            _server.Dispose();
        }
    }
}
