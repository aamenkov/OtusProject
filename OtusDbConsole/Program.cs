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
    private static ConsoleService _consoleService;

    /// <summary>
    /// Сервис работы приложения
    /// </summary>
    private static UnitOfWork _unitOfWork;

    /// <summary>
    /// Сервис инициализации БД
    /// </summary>
    private static InitDbService _initService;

    public class Options
    {
        [Option('t', "table", Required = false, HelpText = "The table name")]
        public int TableType { get; set; }

        [Option('s', "show", Required = false, HelpText = "Show tables")]
        public bool ShowTables { get; set; }
    }

    public static void Main(string[] args)
    {
        // TODO: как правильно переименовывать проекты чтобы ничего не испортить? (с учетом что есть гит)
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

        // Инициализация сервисов
        _configService = new ConfigService(connectionString);
        _initService = new InitDbService(_configService);
        _unitOfWork = new UnitOfWork(_configService);
        _consoleService = new ConsoleService(_unitOfWork);

        // Выводим пользователю вспомогательное сообщение
        WriteHelpToConsole();

        // Зацикливаем работу приложения, до получения команды на выход
        while (true)
        {
            // Получение аргументов командной строки
            Console.WriteLine("");
            Console.WriteLine("Введите команду:");
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
                // Получаем значение номера таблицы 
                var tableNum = o.TableType;
                var showTables = o.ShowTables;

                // Показать значение в таблицах
                if (showTables)
                {
                    ShowTablesToConsole();
                }

                // Если выбрана таблица
                if (tableNum != 0)
                {
                    // В зависимости от номера таблицы добавляем необходимую сущность в БД
                    switch (tableNum)
                    {
                        case (int)ConsoleService.TableEnum.User:
                            var user = _consoleService.GetUserFromConsole();

                            if (user == null)
                            {
                                break;
                            }      
                            _unitOfWork.AddUser(user);
                            Console.WriteLine("Пользователь успешно добавлен в БД");
                            break;

                        case (int)ConsoleService.TableEnum.Homework:
                            var homework = _consoleService.GetHomeworkFromConsole();

                            if (homework == null)
                            {
                                break;
                            }
                            _unitOfWork.AddHomework(homework);
                            Console.WriteLine("ДЗ успешно добавлено в БД");
                            break;

                        case (int)ConsoleService.TableEnum.UserGrades:
                            var grade = _consoleService.GetUserGradeFromConsole();

                            if (grade == null)
                            {
                                break;
                            }
                            _unitOfWork.AddUserGrade(grade);
                            Console.WriteLine("Оценка пользователя успешно добавлена в БД");
                            break;

                        default:
                            Console.WriteLine("Ошибка ввода!");
                            break;
                    }
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
        Console.WriteLine("Usages:");
        Console.WriteLine("Usage 1: -t --table <tabletype> To add value to table.");
        Console.WriteLine("    Table types: " + _consoleService.GetTableNames());
        Console.WriteLine("    Example: -t 1");
        Console.WriteLine("Usage 2: -s --show To show values in tables.");
        Console.WriteLine("Usage 3: 0 - (Exit)");
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
