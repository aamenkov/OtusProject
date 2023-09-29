using Microsoft.Extensions.Configuration;

namespace Customer.WebApi.Services
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    public class ConfigService
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        public string? ConnectionStringPostgres => _postgreString;

        private string? _postgreString = null;


        private IConfiguration _configuration;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
            _postgreString = _configuration.GetSection("ConnectionStringPostgres").Value;
        }
    }
}
