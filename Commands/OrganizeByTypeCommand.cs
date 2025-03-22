using Organizer.Commands.Common;
using Organizer.Commands.Options;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Organizer.Commands
{
    internal class OrganizeByTypeCommand : Command
    {
        public OrganizeByTypeCommand()
                : base("tipo", "Organiza os arquivos por data")
        {
            AddOption(SourceDirectoryOption.Get());
            AddOption(DestinationDirectoryOption.Get());
            AddOption(CompressOption.Get());

            Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo, bool>(Process);
        }

        private void Process(DirectoryInfo source, DirectoryInfo destination, bool compress)
        {
            var filesByType = source.EnumerateFiles()
               .Select(f => new { f.FullName, f.Name, f.Extension })
               .GroupBy(f => new { f.Extension })
               .Select(g => (
                   DestinationPath: Path.Combine(destination.FullName, g.Key.Extension),
                   Files: g.Select(f => (f.Name, f.FullName))
               ));

            FileCommon.CopyFiles(filesByType);

            if (compress)
                FileCommon.Compress(destination);
        }
    }


}
