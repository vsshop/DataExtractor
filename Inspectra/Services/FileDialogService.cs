using Inspectra.Attributes;
using System.IO.Compression;

namespace Inspectra.Services
{
    public class FileDialogService
    {
        [Angular]
        public string[] OpenAngular(string type = "*.csv", bool multiselect = false)
        {
            return Open(type, multiselect).ToArray();
        }
        
        [Angular]
        public void SaveAngular(string[] base64)
        {
            if(base64.Length > 0)
            {
                var dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    var name = $"DLS{DateTime.Now.ToString("MMMM-dd-hh-mm-ss")}";
                    var folder = Path.Combine(dialog.SelectedPath, name);
                    Directory.CreateDirectory(folder);

                    var bytes = Convert.FromBase64String(base64[0]);
                    using var zipStream = new MemoryStream(bytes);
                    ZipFile.ExtractToDirectory(zipStream, folder);
                }
                else MessageBox.Show("wählen andere Ordner");
            }
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
