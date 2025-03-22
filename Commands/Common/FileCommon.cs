using System.IO.Compression;

namespace Organizer.Commands.Common
{
    public class FileCommon
    {
        public static void Compress(DirectoryInfo directory)
        {
            ZipFile.CreateFromDirectory(directory.FullName, $"{directory.Name}.zip");
            directory.Delete(true);
        }

        public static void CopyFiles(IEnumerable<(string DestinationPath, IEnumerable<(string Name, string FullName)> Files)> filesGroup)
        {
            foreach (var (DestinationPath, Files) in filesGroup)
            {
                if (!Directory.Exists(DestinationPath)) Directory.CreateDirectory(DestinationPath);

                foreach (var file in Files)
                    File.Copy(file.FullName, Path.Combine(DestinationPath, file.Name), true);
            }
        }
    }
}
