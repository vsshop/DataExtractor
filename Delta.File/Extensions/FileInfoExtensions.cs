namespace Delta.File.Extensions;

public static class FileInfoExtensions
{
    extension(FileInfo file)
    {
        public string Extension => Path.GetExtension(file.Name).ToLowerInvariant();
        public string DisplayName => Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
    }
}
