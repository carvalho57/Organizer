using Organizer.Commands.Options;
using System.CommandLine;

namespace Organizer.Commands
{
    public abstract class BaseCommand
    {
        protected readonly Option<bool> compressOption = CompressOption.Get();
        protected readonly Option<DirectoryInfo> sourceDirectory = SourceDirectoryOption.Get();
        protected readonly Option<DirectoryInfo> destinationDirectory = DestinationDirectoryOption.Get();

        public static void Process(IEnumerable<(string DestinationPath, IEnumerable<(string Name, string FullName)> Files)> filesGroup)
        {
            foreach (var group in filesGroup)
            {
                if (!Directory.Exists(group.DestinationPath)) Directory.CreateDirectory(group.DestinationPath);

                foreach (var file in group.Files)
                    File.Copy(file.FullName, Path.Combine(group.DestinationPath, file.Name), true);
            }
        }
    }
}
