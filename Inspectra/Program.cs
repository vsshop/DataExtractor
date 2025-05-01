using Inspectra.Forms;
using Inspectra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inspectra
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = App.Create(args);
            app.Services.AddSingleton<FileDialogService>();

            app.Start<Main>();
        }
    }
}