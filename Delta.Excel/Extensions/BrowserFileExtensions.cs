using Microsoft.AspNetCore.Components.Forms;

namespace Delta.Excel.Extensions;

public static class BrowserFileExtensions
{
    extension(IBrowserFile file)
    {
        public string Extension => Path.GetExtension(file.Name).ToLowerInvariant();
    }
}
