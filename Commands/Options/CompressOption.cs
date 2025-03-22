using System.CommandLine;

namespace Organizer.Commands.Options
{
    public static class CompressOption
    {
        public static Option<bool> Get()
        {
            var option  = new Option<bool>(
                aliases: ["--comprimi", "--compress", "-x"],
                description: "Comprime o diretório após a organização"
             );

            return option;
        }
    }
}
