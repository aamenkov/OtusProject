using System.Reflection;
using System.Text;

namespace Reflection.Console.Models
{
    public static class CsvSerializer
    {
        // Сериализация в CSV
        public static string SerializeToCsv(object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var values = properties.Select(p => p.GetValue(obj));

            return string.Join(",", values);
        }

        // Десериализация из CSV
        public static T DeserializeFromCsv<T>(string csvData) where T : new()
        {
            var obj = new T();
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var values = csvData.Split(',');

            for (int i = 0; i < properties.Length; i++)
            {
                var propertyType = properties[i].PropertyType;
                object value;
                if (propertyType == typeof(string))
                {
                    value = values[i];
                }
                else
                {
                    value = Convert.ChangeType(values[i], propertyType);
                }
                properties[i].SetValue(obj, value);
            }

            return obj;
        }
    }
}
