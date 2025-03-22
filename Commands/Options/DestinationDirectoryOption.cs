using System.CommandLine;

namespace Organizer.Commands.Options
{
    public static class DestinationDirectoryOption
    {
        public static Option<DirectoryInfo> Get()
        {
            var option =  new Option<DirectoryInfo>(
                aliases: ["--destino", "--destination","-d"],
                description: "O diretório de destino, onde os arquivos serão copiados ou movidos",
                getDefaultValue: () =>
                {
                    var dest = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "organizado"));
                    return dest;
                }
            );
            return option;

        }
    }
}
