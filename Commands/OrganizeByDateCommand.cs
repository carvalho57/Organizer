using Organizer.Commands.Common;
using Organizer.Commands.Options;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Organizer.Commands
{
    public class OrganizeByDateCommand : Command
    {
        public OrganizeByDateCommand()
            : base("data", "Organiza os arquivos por data")
        {
            AddOption(SourceDirectoryOption.Get());
            AddOption(DestinationDirectoryOption.Get());
            AddOption(CompressOption.Get());

            Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo, bool>(Process);
        }

        private void Process(DirectoryInfo source, DirectoryInfo destination, bool compress)
        {
            var filesByYearMonth = source.EnumerateFiles()
                 .Select(f => new { f.FullName, f.Name, f.CreationTime.Date })
                 .GroupBy(f => new { f.Date.Year, f.Date.Month })
                 .Select(g => (
                     DestinationPath: Path.Combine(destination.FullName, g.Key.Year.ToString(), g.Key.Month.ToString().PadLeft(2, '0')),
                     Files: g.Select(f => (f.Name, f.FullName))
                 ));

            FileCommon.CopyFiles(filesByYearMonth);

            if (compress) 
                FileCommon.Compress(destination);
        }
    }
}
