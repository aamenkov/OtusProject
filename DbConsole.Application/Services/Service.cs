using DbConsole.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DbConsole.Application.Services
{
    public class Service
    {
        private PostgreContext _dbContext;
        public Service(ConfigService configService)
        {
            var optionsPostgreSQL = new DbContextOptionsBuilder<PostgreContext>().UseNpgsql(configService._secretString).Options;
            _dbContext = new PostgreContext(optionsPostgreSQL);
        }

    }
}
