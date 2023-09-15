using System;

namespace DbConsole.Application.Services
{
    public class ConfigService
    {
        public string _secretString { get; }

        public ConfigService(string secretString)
        {
            _secretString = secretString;
        }


    }

}
