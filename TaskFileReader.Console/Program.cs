// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

internal class Program
{

    public static async Task Main(string[] args)
    {
        // Получаем исходную директорию
        string projectPath = Directory.GetCurrentDirectory();
        string folderPath = Path.Combine(projectPath, "TargetFolder");

        // Первый способ
        await ProcessFilesAsync(folderPath);

        // Второй способ
        await ProcessFilesParalellForEachAsync(folderPath);      
    }

    /// <summary>
    /// Способ с массивом из Task
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static async Task ProcessFilesAsync(string folderPath)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string[] fileEntries = Directory.GetFiles(folderPath);
        Task[] tasks = new Task[fileEntries.Length];

        for (int i = 0; i < fileEntries.Length ; i++)
        {
            int index = i;
            tasks[i] = Task.Run(async () =>
            {
                Console.WriteLine($"Поток {index} запущен!");
                var spaceCount = await CountSpacesFromFileAsync(fileEntries[index]);
                Console.WriteLine($"Поток {index} закончил работу! Файл: " + fileEntries[index] + ", пробелов: " + spaceCount);
            });
        }

        await Task.WhenAll(tasks);
        stopwatch.Stop();
        Console.WriteLine("Время выполнения метода 'Способ с массивом из Task': " + stopwatch.ElapsedMilliseconds + " миллисекунд");
    }


    /// <summary>
    /// Способ с ParalelForEach
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static async Task ProcessFilesParalellForEachAsync(string folderPath)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await Task.Run(() => 
        { 
            string[] fileEntries = Directory.GetFiles(folderPath);
            Parallel.ForEach(fileEntries, async (fileEntry, parallelLoopState, index) =>
            {
                Console.WriteLine($"Поток {index} запущен!");
                var spaceCount = await CountSpacesFromFileAsync(fileEntry);
                Console.WriteLine($"Поток {index} закончил работу! Файл: " + fileEntry + ", пробелов: " + spaceCount);
            });
        }
        );

        stopwatch.Stop();
        Console.WriteLine("Время выполнения метода 'Способ с ParalelForEach': " + stopwatch.ElapsedMilliseconds + " миллисекунд");
    }


    /// <summary>
    /// Подсчитываем количество пробелов в файле
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<int> CountSpacesFromFileAsync(string path)
    {
        string content = await File.ReadAllTextAsync(path);
        return content.Split(' ').Length - 1;
    }
}
