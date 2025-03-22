using System.IO.Compression;

namespace Organizer
{
    public class FileUtils
    {
        public static void Compress(DirectoryInfo directory)
        {
            ZipFile.CreateFromDirectory(directory.FullName, $"{directory.Name}.zip");
            directory.Delete(true);
        }

    }
}
