using CommandLine;
using System;

namespace Otus.DbConsole
{
    public static class ConfigService
    {
        public static string GetTableNames()
        {
            string result = "";

            foreach (TableEnum table in Enum.GetValues(typeof(TableEnum)))
            {
                result = string.Concat(result, (int)table, " - ", table.ToString(), ", ");
            }

            return result.Remove(result.Length - 2, 2);
        }

        public static Parser Parser = new Parser(settings =>
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
