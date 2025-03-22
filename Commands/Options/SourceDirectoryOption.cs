using System.CommandLine;

namespace Organizer.Commands.Options
{
    public static class SourceDirectoryOption
    {
        public static Option<DirectoryInfo> Get()
        {
            var option = new Option<DirectoryInfo>(
              aliases: ["--origem", "--source", "-o"],
              description: "O diretório de origem, que contém os arquivos para serem organizados"
            );
            option.IsRequired = true;
            option.AddValidator(source =>
            {
                DirectoryInfo? directory = source.GetValueForOption(option);

                if (directory is null)
                    source.ErrorMessage = $"É obrigatório informar o diretório de origem";
                else if (!Directory.Exists(directory.FullName))
                    source.ErrorMessage = $"Diretório {directory.FullName} não existe";
            });

            return option;

        }
    }
}
