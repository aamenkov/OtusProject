using CommandLine;
using System;

namespace Otus.DbConsole.Services
{
    public class ConfigService
    {
        public string _secretString { get; }

        public ConfigService(string secretString)
        {
            _secretString = secretString;
        }

        public string GetTableNames()
        {
            string result = "";

            foreach (TableEnum table in Enum.GetValues(typeof(TableEnum)))
            {
                result = string.Concat(result, (int)table, " - ", table.ToString(), ", ");
            }

            return result.Remove(result.Length - 2, 2);
        }

        public Parser Parser = new Parser(settings =>
        {
            // Отключите генерацию справочной информации
            settings.AutoHelp = false;
            settings.AutoVersion = false;
        });

        public enum TableEnum
        {
            User = 1,
            Homework = 2,
            UserGrades = 3
        }
    }

}
