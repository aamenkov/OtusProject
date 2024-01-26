// See https://aka.ms/new-console-template for more information

using Cutomer.Console.Client;

internal class Program
{
    public static void Main(string[] args)
    {
        Random random = new Random();
        var baseUrl = $"https://localhost:7195/";
        var client = new ServiceClient(baseUrl);

        // Зацикливаем работу приложения, до получения команды на выход
        while (true)
        {
            int randomNumber = random.Next(1, 101);

            // Получение аргументов командной строки
            Console.WriteLine("Приложение запущено! Для выхода из приложения введите - 0.");
            Console.WriteLine("");
            Console.WriteLine("Введите id-пользователя:");
            var arg = Console.ReadLine();
            Console.WriteLine("");

            // Проверяем на условие выхода из программы
            var exitResult = int.TryParse(arg, out int res);
            if (exitResult && res == 0)
            {
                Console.WriteLine("Good luck!");
                Environment.Exit(0);
            }

            // Принимает с консоли ID "Клиента", запрашивает его с сервера и отображает его данные по пользователю;
            var model = client.GetCustomerAsync(res);
            if (model.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Ошибка - {model.Message}");
            }
            else
            {
                Console.WriteLine($"Получен ответ от сервиса = '{model.Value}'");
            }

            // Генерирует случайным образом данные для создания нового "Клиента" на сервере;
            Console.WriteLine("");
            Console.WriteLine($"Cгенерирован рандомный id - {randomNumber}");
            // Отправляет данные на сервер;
            var res1 = client.AddCustomerAsync(randomNumber, "name", "surname");
            Console.WriteLine("Отправлен запрос на создание пользователя с таким ID.");
            if (res1.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Ошибка - {res1.Message}");
            }
            else
            {
                Console.WriteLine("Пользователь успешно создан.");
                Console.WriteLine($"Получен ответ от сервиса = '{res1.Value}'");
            }

            // По полученному ID от сервера запросить созданного пользователя с сервера и вывести на экран.
            Console.WriteLine("");
            var model1 = client.GetCustomerAsync(res1.Value);
            Console.WriteLine($"Отправлен запрос на получение пользователя. ID = {res1.Value.ToString()}.");
            Console.WriteLine($"Получен ответ от сервиса = '{model1.Value}'");
            Console.WriteLine("");
        }
    }      
}