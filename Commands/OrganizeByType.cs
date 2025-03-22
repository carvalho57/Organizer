using System.CommandLine;

namespace Organizer.Commands
{
    internal class OrganizeByType : BaseCommand
    { 
        public Command Create()
        {
            var command = new Command("tipo", "Organiza os arquivos por tipo");
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
            var filesByType = source.EnumerateFiles()
                  .Select(f => new { f.FullName, f.Name, f.Extension })
                  .GroupBy(f => new { f.Extension })
                  .Select(g => (
                      DestinationPath: Path.Combine(destination.FullName, g.Key.Extension),
                      Files: g.Select(f => (f.Name, f.FullName))
                  ));

            Process(filesByType);

            return Task.CompletedTask;
        }

    }
}
