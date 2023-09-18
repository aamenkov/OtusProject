using System;

namespace DbConsole.Application.Services
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    public class ConfigService
    {
        public string _secretString { get; }

        public ConfigService(string secretString)
        {
            _secretString = secretString;
        }


    }

}
