using CommandLine;
using DbConsole.Application.Models;
using DbConsole.Application.Services;

namespace DbConsole.Common
{
    /// <summary>
    /// Логика работы пользователя с консолью
    /// </summary>
    public class ConsoleService
    {
        /// <summary>
        /// Сервис работы приложения
        /// </summary>
        private static UnitOfWork _unitOfWork;

        public ConsoleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Парсер, используемый для работы с аргументами коммандной строки
        /// </summary>
        public Parser Parser = new Parser(settings =>
        {
            // Отключите генерацию справочной информации
            settings.AutoHelp = false;
            settings.AutoVersion = false;
        });

        /// <summary>
        /// Перечисления таблиц которые может заполнить пользователь
        /// </summary>
        public enum TableEnum
        {
            User = 1,
            Homework = 2,
            UserGrades = 3
        }

        /// <summary>
        /// Получение списка названий таблиц
        /// </summary>
        /// <returns></returns>
        public string GetTableNames()
        {
            string result = "";

            foreach (TableEnum table in Enum.GetValues(typeof(TableEnum)))
            {
                result = string.Concat(result, (int)table, " - ", table.ToString(), ", ");
            }

            return result.Remove(result.Length - 2, 2);
        }

        /// <summary>
        /// Заполнение сущности "Пользователь" из консоли
        /// </summary>
        /// <returns></returns>
        public static UserModel GetUserFromConsole()
        {
            var user = new UserModel();
            Console.WriteLine("Введите имя пользователя: ");
            var name = Console.ReadLine();

            if (name == null || name.Length == 0)
            {
                name = "";
            }

            Console.WriteLine("Пользователь является преподавателем? (true/false) ");
            var isLecturerString = Console.ReadLine();
            if (!bool.TryParse(isLecturerString, out var isLecturer))
            {
                Console.WriteLine("Некорректный ввод!");
                return null;
            }

            user.Name = name;
            user.IsLecturer = isLecturer;
            return user;
        }

        /// <summary>
        /// Заполнение сущности "ДЗ" из консоли
        /// </summary>
        /// <returns></returns>
        public static HomeworkModel GetHomeworkFromConsole()
        {
            var homework = new HomeworkModel();
            Console.WriteLine("Введите заголовок: ");
            var name = Console.ReadLine();

            if (name == null || name.Length == 0)
            {
                name = "";
            }

            Console.WriteLine("Введите описание: (необязательно)");
            var description = Console.ReadLine();

            if (description == null || description.Length == 0)
            {
                description = "";
            }

            homework.Title = name;
            homework.Description = description;
            return homework;
        }

        /// <summary>
        /// Заполнение сущности "оценка пользователя" из консоли
        /// </summary>
        /// <returns></returns>
        public static UserGradeModel GetUserGradeFromConsole()
        {
            var grade = new UserGradeModel();

            Console.WriteLine("Введите ID - домашнего задания: ");
            var homeworkIdString = Console.ReadLine();
            if (!int.TryParse(homeworkIdString, out var homeworkId))
            {
                Console.WriteLine("Некорректный ввод HomeworkId!");
                return null;
            }
            else
            {
                var homework = _unitOfWork.GetHomework(homeworkId);
                if (homework == null) { return null; }
            }

            Console.WriteLine("Введите ID - пользователя: ");
            var guidString = Console.ReadLine();

            if (!Guid.TryParse(guidString, out var guid))
            {
                Console.WriteLine("Некорректный ввод GuidId!");
                return null;
            }
            else
            {
                var user = _unitOfWork.GetUser(guid);
                if (user == null) { return null; }
            }

            Console.WriteLine("Введите комментарий: (необязательно)");
            var description = Console.ReadLine();
            if (description == null || description.Length == 0)
            {
                description = "";
            }

            Console.WriteLine("Введите оценку: (0-100)");
            var quantityString = Console.ReadLine();
            if (int.TryParse(quantityString, out var quantity))
            {
                if (!(quantity >= 0 && quantity < 101))
                {
                    quantity = 0;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод!");
                return null;
            }

            Console.WriteLine("Задание считается пройденным? (true/false) ");
            var isPassedString = Console.ReadLine();
            if (!bool.TryParse(isPassedString, out var isPassed))
            {
                Console.WriteLine("Некорректный ввод!");
                return null;
            }

            grade.HomeworkId = homeworkId;
            grade.UserId = guid;
            grade.Quantity = quantity;
            grade.Comment = description;
            grade.IsPassed = isPassed;

            return grade;
        }
    }
}
