using CommandLine;
using Microsoft.Extensions.Configuration;
using Otus.DbConsole.Services;

internal class Program
{
    private static ConfigService _configService;
    public class Options
    {
        [Option('t', "table", Required = true, HelpText = "The table name.")]
        public int TableType { get; set; }
    }

    public static void Main(string[] args)
    {
        _configService = new ConfigService("Host=localhost;Port=5432;Database=OtusProject;Username=postgres;Password=postgres");
        var initService = new InitService(_configService);

        // вывести все таблицы 
        // добавить возможность добавить в таблицу на выбор сущность 

        // сделать заполнение таблиц 
        // вывести строку подключения в переменные среды
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
            _configService.Parser.ParseArguments<Options>(arguments).WithParsed(o =>
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
        Console.WriteLine("Table types: " + _configService.GetTableNames());
        Console.WriteLine("Exit = 0");
    }
}
