using System.Windows;
using Delta.Application;
using Delta.Excel;
using Delta.Services;
using Microsoft.Extensions.DependencyInjection;
using Telemetry.Builder;

namespace Delta;

public partial class MainWindow : Window
{
    readonly WebHybridBuilder builder;
    public MainWindow()
    {
        InitializeComponent();

        builder = WebHybridBuilder.CreateBuilder();

        builder.Services.AddApplication();
        builder.Services.AddExcelServices();
        builder.Services.AddScoped<ReaderService>();
        builder.Services.AddScoped<DataService>();
        builder.Services.AddScoped<UITableService>();

        var app = builder.Build();

        Resources.Add("services", app.Services);
    }

    protected override async void OnClosed(EventArgs e)
    {
        await builder.DisposeAsync();
    }
}