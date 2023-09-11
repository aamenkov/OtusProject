using Microsoft.EntityFrameworkCore;
using Otus.DbConsole.Infrastructure.Context;
using Otus.DbConsole.Services;

namespace Otus.DbConsole.Services
{
    public class InitService
    {
        private PostgreContext _context;
        public InitService(ConfigService configService)
        {
            var optionsPostgreSQL = new DbContextOptionsBuilder<PostgreContext>().UseNpgsql(configService._secretString).Options;

            _context = new PostgreContext();
            if (_context.Database.EnsureCreated())
            {
                InitDatabase();
            }

        }

        private void InitDatabase()
        {
        }
    }
}
