using System.Windows;
using Delta.File;
using Delta.Application;
using Telemetry.Builder;
using Microsoft.Extensions.DependencyInjection;
using Delta.Services;

namespace Delta;

public partial class MainWindow : Window
{
    readonly WebHybridBuilder builder;
    public MainWindow()
    {
        InitializeComponent();

        builder = WebHybridBuilder.CreateBuilder();

        builder.Services.AddApplication();
        builder.Services.AddReaderServices();

        builder.Services.AddScoped<DataService>();
        builder.Services.AddScoped<LoaderService>();

        builder.Services.AddScoped<UISvgService>();
        builder.Services.AddScoped<UITimerService>();
        builder.Services.AddScoped<UITableService>();
        builder.Services.AddScoped<UIWriterService>();

        var app = builder.Build();

        Resources.Add("services", app.Services);
    }

    protected override async void OnClosed(EventArgs e)
    {
        await builder.DisposeAsync();
    }
}