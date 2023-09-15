using Microsoft.EntityFrameworkCore;
using DbConsole.Infrastructure.Context;
using DbConsole.Infrastructure.Entities;

namespace DbConsole.Application.Services
{
    public class InitDbService
    {
        private PostgreContext _dbContext;
        public InitDbService(ConfigService configService)
        {
            var optionsPostgreSQL = new DbContextOptionsBuilder<PostgreContext>().UseNpgsql(configService._secretString).Options;
            _dbContext = new PostgreContext(optionsPostgreSQL);
            if (_dbContext.Database.EnsureCreated())
            {
                InitDatabase();
            }
        }

        /// <summary>
        /// Инициализация начальных значений базы данных
        /// </summary>
        private void InitDatabase()
        {
            //Заполняем базу данных тестовыми значениями
            _dbContext.Homeworks.AddRange(
                new Homework
                {
                    Title = "Знакомство",
                    Description = "Знакомство, рассказ о формате Scrum, краткий обзор курса",
                    UserGrades = new List<UserGrade>() { }
                },
                new Homework
                {
                    Title = "Работа с БД",
                    Description = "Базы данных: реляционные базы и работа с ними",
                    UserGrades = new List<UserGrade>() { }
                },
                new Homework
                {
                    Title = "Работа с API",
                    Description = "REST и RESTful API",
                    UserGrades = new List<UserGrade>() { }
                },
                new Homework
                {
                    Title = "Паттерны проектирования",
                    Description = "Принципы SOLID",
                    UserGrades = new List<UserGrade>() { }
                },
                new Homework
                {
                    Title = "Reflection",
                    Description = "Отражение (Reflection)",
                    UserGrades = new List<UserGrade>() { }
                },
                new Homework
                {
                    Title = "События и делегаты",
                    Description = "Работа с методами как с переменными (delegates, events)",
                    UserGrades = new List<UserGrade>() { }
                }
            );

            var userGuid1 = Guid.NewGuid();
            var userGuid2 = Guid.NewGuid();
            var userGuid3 = Guid.NewGuid();
            var userGuid4 = Guid.NewGuid();
            var userGuid5 = Guid.NewGuid();
            var userGuid6 = Guid.NewGuid();

            _dbContext.Users.AddRange(
                new User
                {
                    UserId = userGuid1,
                    Name = "Андрей Андреев",
                    IsLecturer = false,
                    UserGrades = new List<UserGrade>() { }
                },
                new User
                {
                    UserId = userGuid2,
                    Name = "Иван Иванов",
                    IsLecturer = false,
                    UserGrades = new List<UserGrade>() { }
                },
                new User
                {
                    UserId = userGuid3,
                    Name = "Петр Петров",
                    IsLecturer = false,
                    UserGrades = new List<UserGrade>() { }
                },
                new User
                {
                    UserId = userGuid4,
                    Name = "Дмитрий Дмитриев",
                    IsLecturer = false,
                    UserGrades = new List<UserGrade>() { }
                },
                new User
                {
                    UserId = userGuid5,
                    Name = "Александр Александров",
                    IsLecturer = false,
                    UserGrades = new List<UserGrade>() { }
                },
                new User
                {
                    UserId = userGuid6,
                    Name = "Константин Константинов",
                    IsLecturer = true,
                    UserGrades = new List<UserGrade>() { }
                }
            );

            // Добавление оценок
            _dbContext.UserGrades.AddRange(
                new UserGrade
                {
                    Comment = "Все сделано отлично!",
                    IsPassed = true,
                    Quantity = 100,
                    HomeworkId = 1,
                    UserId = userGuid1
                },
                new UserGrade
                {
                    Comment = "Не до конца заполнил базу",
                    IsPassed = true,
                    Quantity = 50,
                    HomeworkId = 2,
                    UserId = userGuid1
                },
                new UserGrade
                {
                    Comment = "Не поздоровался",
                    IsPassed = false,
                    Quantity = 1,
                    HomeworkId = 1,
                    UserId = userGuid2
                },
                new UserGrade
                {
                    Comment = "Connection string убрать в переменные окружения",
                    IsPassed = true,
                    Quantity = 75,
                    HomeworkId = 3,
                    UserId = userGuid3
                },
                new UserGrade
                {
                    Comment = "Все сделано отлично!",
                    IsPassed = true,
                    Quantity = 100,
                    HomeworkId = 1,
                    UserId = userGuid4
                },
                new UserGrade
                {
                    Comment = "Не доделал работу",
                    IsPassed = true,
                    Quantity = 45,
                    HomeworkId = 4,
                    UserId = userGuid4
                },
                new UserGrade
                {
                    Comment = "Не выполнено",
                    IsPassed = false,
                    Quantity = 0,
                    HomeworkId = 1,
                    UserId = userGuid5
                },
                new UserGrade
                {
                    Comment = "Без комментариев",
                    IsPassed = true,
                    Quantity = 100,
                    HomeworkId = 2,
                    UserId = userGuid5
                }
            );

            _dbContext.SaveChanges();
        }

    }
}
