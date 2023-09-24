namespace DbConsole.Application.Services
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    public class ConfigService
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        public string _secretString { get; }

        public ConfigService(string secretString)
        {
            _secretString = secretString;
        }


    }

}
