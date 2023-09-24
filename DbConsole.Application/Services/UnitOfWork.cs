using DbConsole.Application.Models;
using DbConsole.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DbConsole.Application.Services
{
    /// <summary>
    /// Работа с БД внутри приложения
    /// </summary>
    public class UnitOfWork
    {
        private PostgreContext _dbContext;
        public UnitOfWork(ConfigService configService)
        {
            var optionsPostgreSQL = new DbContextOptionsBuilder<PostgreContext>().UseNpgsql(configService._secretString).Options;
            _dbContext = new PostgreContext(optionsPostgreSQL);
        }

        public List<HomeworkModel> GetAllHomework() 
        {
            return _dbContext.Homeworks.Select(x => HomeworkModel.ConvertToApplicationModel(x)).ToList();
        }

        public void AddHomework(HomeworkModel homework)
        {
            _dbContext.Homeworks.Add(HomeworkModel.ConvertToEntity(homework));
            _dbContext.SaveChanges();
        }

        public HomeworkModel GetHomework(int id)
        {
            var homework = _dbContext.Homeworks.FirstOrDefault(x => x.Id == id);
            if (homework == null) { return null; }
            return HomeworkModel.ConvertToApplicationModel(homework);
        }

        public List<UserModel> GetAllUsers()
        {
            return _dbContext.Users.Select(x => UserModel.ConvertToApplicationModel(x)).ToList();
        }

        public UserModel GetUser(Guid guid)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == guid);
            if (user == null) { return null; }
            return UserModel.ConvertToApplicationModel(user);
        }

        public Guid AddUser(UserModel user)
        {
            user.UserId = Guid.NewGuid();
            _dbContext.Users.Add(UserModel.ConvertToEntity(user));
            _dbContext.SaveChanges();
            return user.UserId;
        }

        public List<UserGradeModel> GetAllUserGrades()
        {
            return _dbContext.UserGrades.Select(x => UserGradeModel.ConvertToApplicationModel(x)).ToList();
        }

        public void AddUserGrade(UserGradeModel userGrade)
        {
            _dbContext.UserGrades.Add(UserGradeModel.ConvertToEntity(userGrade));
            _dbContext.SaveChanges();
        }
    }
}
