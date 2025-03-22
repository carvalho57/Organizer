using Organizer.Commands;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace Organizer
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("""
                ___  _ __ __ _  __ _ _ __ (_)_______ 
             / _ \| '__/ _` |/ _` | '_ \| |_  / _ \
            | (_) | | | (_| | (_| | | | | |/ /  __/
             \___/|_|  \__, |\__,_|_| |_|_/___\___|
                       |___/                       

                Organiza os arquivos por data ou tipo, também permite comprimir

            """);

            rootCommand.AddCommand(new OrganizeByDate().Create());
            rootCommand.AddCommand(new OrganizeByType().Create());

            return await rootCommand.InvokeAsync(args).ConfigureAwait(false);

        }
    }
}
