using CommandLine;
using DbConsole.Application.Services;
using DbConsole.Common;
using Microsoft.Extensions.Configuration;

internal class Program
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    private static ConfigService _configService;

    /// <summary>
    /// Вспомогательный класс для работы с консолью
    /// </summary>
    private static ConsoleService _consoleService = new ConsoleService();

    /// <summary>
    /// Сервис работы приложения
    /// </summary>
    private static UnitOfWork _unitOfWork;
    public class Options
    {
        [Option('t', "table", Required = false, HelpText = "The table name")]
        public int TableType { get; set; }

        [Option('s', "show", Required = false, HelpText = "Show tables")]
        public bool ShowTables { get; set; }
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
        _unitOfWork = new UnitOfWork(_configService);

        // TODO: сделать методы для добавления записи в таблицу
        // TODO: задать вопрос. как правильно переименовывать проекты чтобы ничего не испортить?

        WriteHelpToConsole();

        while (true)
        {
            Console.WriteLine("");
            var arg = Console.ReadLine();
            
            // Проверяем на условие выхода из программы
            var exitResult = int.TryParse(arg, out int res);
            if (exitResult && res == 0)
            {
                Console.WriteLine("Good luck!");
                Environment.Exit(0);
            }

            // Парсим полученные аргументы строки
            var arguments = new[] { arg };
            _consoleService.Parser.ParseArguments<Options>(arguments).WithParsed(o =>
            {
                var table = o.TableType;
                var showTables = o.ShowTables;

                // Показать значение в таблицах
                if (showTables)
                {
                    ShowTablesToConsole();
                }

                // Если выбрана таблица
                if (table != 0)
                {

                }

            }).WithNotParsed(errors =>
            {
                Console.WriteLine("Please check, that u use correct settings in launchSettings.json file.");
                Console.WriteLine();
                WriteHelpToConsole();
            });
        }
    }

    /// <summary>
    /// Показать помощь
    /// </summary>
    public static void WriteHelpToConsole()
    {
        // TODO: Вынести список таблиц в БД. 
        Console.WriteLine("Usage 1 : -t --table <tabletype> To add value to table.");
        Console.WriteLine("Table types: " + _consoleService.GetTableNames());
        Console.WriteLine("Usage 2 : -s --show To show values in tables.");

        Console.WriteLine("Exit = 0");
    }

    /// <summary>
    /// Показать инфо из таблиц
    /// </summary>
    public static void ShowTablesToConsole()
    {
        Console.WriteLine("Пользователи: ");
        var users = _unitOfWork.GetAllUsers();
        users.ForEach(x => Console.WriteLine(x.ToString()));
        Console.WriteLine();

        Console.WriteLine("Домашние задания: ");
        var homeworks = _unitOfWork.GetAllHomework();
        homeworks.ForEach(x => Console.WriteLine(x.ToString()));
        Console.WriteLine();

        Console.WriteLine("Оценки: ");
        var userGrades = _unitOfWork.GetAllUserGrades();
        userGrades.ForEach(x => Console.WriteLine(x.ToString()));
        Console.WriteLine();
    }
}
