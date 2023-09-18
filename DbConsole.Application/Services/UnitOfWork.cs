using DbConsole.Application.Models;
using DbConsole.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DbConsole.Application.Services
{
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

        public List<UserModel> GetAllUsers()
        {
            return _dbContext.Users.Select(x => UserModel.ConvertToApplicationModel(x)).ToList();
        }

        public List<UserGradeModel> GetAllUserGrades()
        {
            return _dbContext.UserGrades.Select(x => UserGradeModel.ConvertToApplicationModel(x)).ToList();
        }
    }
}
