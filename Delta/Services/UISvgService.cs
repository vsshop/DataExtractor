using System.IO;
using System.Text;

namespace Delta.Services;

public sealed class UISvgService
{
    private string Root => Path.Combine(AppContext.BaseDirectory, "wwwroot", "icons");

    public async Task<string> LoadAsync(string url)
    {
        var relative = url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(Root, relative + ".svg");

        if (!System.IO.File.Exists(fullPath))
        {
            throw new FileNotFoundException($"SVG not found: {fullPath}");
        }

        return await System.IO.File.ReadAllTextAsync(fullPath, Encoding.UTF8);
    }
}
