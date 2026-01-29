using Microsoft.Extensions.Hosting;

namespace Delta.Excel.Extensions;

public static class HostEnvironmentExtension
{
    extension(IHostEnvironment environment)
    {
        public string Settings
        {
            get
            {
                var folder = Path.Combine(AppContext.BaseDirectory, "settings");
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }

        public string ReplaceRules
        {
            get
            {
                var folder = Path.Combine(AppContext.BaseDirectory, "settings", "replace-rules");
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }

        public string SortingRules
        {
            get
            {
                var folder = Path.Combine(AppContext.BaseDirectory, "settings", "sorting-rules");
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }

        public string ValidationRules
        {
            get
            {
                var folder = Path.Combine(AppContext.BaseDirectory, "settings", "validation-rules");
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }
    }
}
