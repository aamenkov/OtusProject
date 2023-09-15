using CommandLine;
using DbConsole.Application.Services;
using DbConsole.Common;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static ConfigService _configService;
    private static ConsoleService _consoleService = new ConsoleService();
    public class Options
    {
        [Option('t', "table", Required = true, HelpText = "The table name.")]
        public int TableType { get; set; }
    }

    public static void Main(string[] args)
    {
        // Создаем конфигурацию
        var configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();

        // Считываем строку подключения к базе данных
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString == null) { 
            Console.WriteLine("Check that u use correct connection string in appsettings.json");
            Environment.Exit(0);
        };

        _configService = new ConfigService(connectionString);
        var initService = new InitDbService(_configService);

        // вывести все таблицы 
        // + сделать конвертацию моделей 
        // сделать методы для вывода записей из таблиц 
        // сделать методы для добавления записи в таблицу

        // + сделать заполнение таблиц 
        // + вывести строку подключения 

        // как правильно переименовывать проекты чтобы ничего не испортить?
        // в случае с гитом

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
            _consoleService.Parser.ParseArguments<Options>(arguments).WithParsed(o =>
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
        Console.WriteLine("Table types: " + _consoleService.GetTableNames());
        Console.WriteLine("Exit = 0");
    }
}
