using System.CommandLine;

namespace Organizer.Commands
{
    public class OrganizeByDate : BaseCommand
    {     
        public Command Create()
        {
            var command = new Command("data", "Organiza os arquivos por data");
            command.AddOption(sourceDirectory);
            command.AddOption(destinationDirectory);
            command.AddOption(compressOption);


            command.SetHandler(async (source, destination, compress) =>
            {
                await Organize(source, destination);

                if (compress) FileUtils.Compress(destination);

            }, sourceDirectory, destinationDirectory, compressOption);

            return command;
        }

        private Task Organize(DirectoryInfo source, DirectoryInfo destination)
        {
            var filesByYearMonth = source.EnumerateFiles()
                 .Select(f => new { f.FullName, f.Name, f.CreationTime.Date })
                 .GroupBy(f => new { f.Date.Year, f.Date.Month })
                 .Select(g => (
                     DestinationPath: Path.Combine(destination.FullName, g.Key.Year.ToString(), g.Key.Month.ToString().PadLeft(2, '0')),
                     Files: g.Select(f => (f.Name, f.FullName))
                 ));

            Process(filesByYearMonth);

            return Task.CompletedTask;
        }

    }
}
