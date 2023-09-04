using CommandLine;
using Microsoft.Extensions.Configuration;
using Otus.DbConsole;

internal class Program
{
    public class Options
    {
        [Option('t', "table", Required = true, HelpText = "The table name.")]
        public int TableType { get; set; }
    }

    public static void Main(string[] args)
    {
        // вывести все таблицы 
        // добавить возможность добавить в таблицу на выбор сущность 
        WriteHelpToConsole();

        while (true)
        {
            Console.WriteLine("");
            var arg = Console.ReadLine();
            
            var exitResult = int.TryParse(arg, out int res);
            if (exitResult && res == 0)
            {
                Console.WriteLine("Good luck!");
                Environment.Exit(0);
            }

            var arguments = new[] { arg };
            ConfigService.Parser.ParseArguments<Options>(arguments).WithParsed(o =>
            {
                var table = o.TableType;

                // Console.WriteLine("Hello, World!");
                // var builder = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json").Build();
                //string connectionString = builder.GetConnectionString("DefaultConnection");

            }).WithNotParsed(errors =>
            {
                Console.WriteLine("Please check, that u use correct settings in launchSettings.json file.");
                Console.WriteLine();
                WriteHelpToConsole();
            });
        }


    }

    public static void WriteHelpToConsole()
    {
        Console.WriteLine("Usage: -t --table <tabletype>");
        Console.WriteLine("Possible values: " + ConfigService.GetTableNames());
        Console.WriteLine("Exit = 0");
    }
}
