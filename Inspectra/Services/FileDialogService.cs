using Inspectra.Attributes;

namespace Inspectra.Services
{
    public class FileDialogService
    {
        [Angular]
        public string[] OpenAngular(string type = "*.csv", bool multiselect = false)
        {
            return Open(type, multiselect).ToArray();
        }

        public IEnumerable<string> Open(string type, bool multiselect = false)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = $"Select File ({type})|{type}";
            open.Multiselect = multiselect;

            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in open.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    yield return fileInfo.FullName;
                }
            }
        }

        [Angular]
        public bool Save(out string path)
        {
            path = string.Empty;
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var name = $"DLS{DateTime.Now.ToString("MMMM-dd-hh-mm-ss")}";
                var folder = Path.Combine(dialog.SelectedPath, name);
                Directory.CreateDirectory(folder);
                return true;
            }
            return false;
        }
    }
}
