using Inspectra.Hosts;
using Inspectra.Services;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inspectra.Controls
{
    public partial class AngularView : UserControl
    {
        private HostServer _host;
        private AngularService _angular;
        public AngularView(HostServer host, AngularService angular)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            _angular = angular;
            _host = host;
        }

        private async void AngularView_Load(object sender, EventArgs e)
        {
            await web.EnsureCoreWebView2Async();
            web.CoreWebView2.Navigate(_host.URL);
            web.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

        }
        private async void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var json = e.WebMessageAsJson;
            var message = JsonSerializer.Deserialize<AngularInvokeMessage>(json);
            if (message == null || message.Method == null) return;
            var result = await _angular.InvokeAsync(message.Method, message.Payload);
            var response = new AngularResponseMessage
            {
                Method = message.Method,
                Result = result ?? new object()
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);
            web.CoreWebView2.PostWebMessageAsJson(jsonResponse);
        }



        public class AngularResponseMessage
        {
            public string? Method { get; set; }
            public object? Result { get; set; }
        }

        public class AngularInvokeMessage
        {
            [JsonPropertyName("type")]
            public string? Type { get; set; }

            [JsonPropertyName("method")]
            public string? Method { get; set; }


            [JsonPropertyName("payload")]
            public string? Payload { get; set; }
        }
    }
}
