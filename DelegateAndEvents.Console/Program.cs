// See https://aka.ms/new-console-template for more information

using DelegateAndEvents.Console.Models;

internal class Program
{
    public static void Main(string[] args) 
    {
        // Пример использования обобщенной функции расширения
        List<string> strings = new List<string> { "one", "four", "three", "seven", "verybigstring" };
        string maxString = strings.GetMax(s => (float)s.Length);
        Console.WriteLine("Max string: " + maxString);

        // Пример использования класса FileSearcher
        var searcher = new FileSearcher();
        searcher.FileFound += (sender, e) => Console.WriteLine("Найден файл: " + e.FileName);
        searcher.SearchCancelled += (sender, e) => Console.WriteLine("Поиск остановлен. Галя отмена.");

        // Получаем исходную директорию
        string projectPath = Directory.GetCurrentDirectory();
        string folderPath = Path.Combine(projectPath, "TargetFolder");

        // Начинаем поиск файлов
        searcher.SearchFiles(folderPath);

        // Отмена поиска
        searcher.CancelSearch();

        // Попытка начать поиск файлов
        searcher.SearchFiles(folderPath);
    }
}