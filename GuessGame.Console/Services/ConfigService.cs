using Microsoft.Extensions.Configuration;

namespace GuessGame.Console.Services
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    public class ConfigService
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        private IConfigurationRoot _config { get; }

        public int NumberOfAttemts { get; private set; }
        public int MinRange { get; private set; }
        public int MaxRange { get; private set; }


        public ConfigService(IConfigurationRoot config)
        {
            _config = config;

            // Получение значений из конфигурации
            var minRange = _config.GetSection("GuessGameSettings:MinRange").Value; 
            var maxRange = _config.GetSection("GuessGameSettings:MaxRange").Value;
            var numberOfAttempts = _config.GetSection("GuessGameSettings:NumberOfAttempts").Value;

            if (minRange == null || maxRange == null || numberOfAttempts == null)
            {
                throw new Exception("Неправильно указаны параметры в конфигурационном файле.");
            }

            // Преобразование значений в int
            MinRange = int.Parse(minRange);
            MaxRange = int.Parse(maxRange);
            NumberOfAttemts = int.Parse(numberOfAttempts);
        }
    }
}
