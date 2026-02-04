using Microsoft.AspNetCore.Components.Forms;

namespace Delta.File.Extensions;

public static class BrowserFileExtensions
{
    extension(IBrowserFile file)
    {
        public string Extension => Path.GetExtension(file.Name).ToLowerInvariant();
        public string DisplayName => Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
    }
}
