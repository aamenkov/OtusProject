using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

internal class Program
{
    public static void Main()
    {
        Console.WriteLine("Характеристики компьютера:");
        Console.WriteLine($"Операционная система: {Environment.OSVersion}");
        Console.WriteLine($"Процессор: {Environment.ProcessorCount} ядер");
        Console.WriteLine($"Общий объем памяти: {Environment.SystemPageSize * Environment.SystemPageSize} байт");
        Console.WriteLine();

        Console.WriteLine("100 000");
        RunApp(100000);
        Console.WriteLine();

        Console.WriteLine("1 000 000");
        RunApp(1000000);
        Console.WriteLine();

        Console.WriteLine("10 000 000");
        RunApp(10000000);
    }

    public static void RunApp(int amount)
    {
        // Генерация массива случайных чисел
        int[] numbers = GenerateRandomNumbers(amount);

        // Обычное вычисление суммы элементов массива
        MeasureTime(() => CalculateSum(numbers), "Обычное вычисление");
        // Параллельное вычисление суммы элементов массива с помощью Thread
        MeasureTime(() => CalculateSumParallelWithThreads(numbers), "Параллельное вычисление с помощью Thread");
        // Параллельное вычисление суммы элементов массива с помощью ParalellForEach
        MeasureTime(() => CalculateSumParallelWithParalellForEach(numbers), "Параллельное вычисление с помощью ParalellForEach");
        // Параллельное вычисление суммы элементов массива с помощью LINQ
        MeasureTime(() => CalculateSumParallelWithLinq(numbers), "Параллельное вычисление с помощью LINQ");
    }


    // Генерация массива случайных чисел
    static int[] GenerateRandomNumbers(int count)
    {
        Random random = new Random();
        int[] numbers = new int[count];
        for (int i = 0; i < count; i++)
        {
            numbers[i] = random.Next(1, 100);
        }
        return numbers;
    }


    // Вычисление суммы элементов массива
    static long CalculateSum(int[] numbers)
    {
        return numbers.Sum(i => (long)i);
    }


    // Параллельное вычисление суммы элементов массива с помощью Thread
    static long CalculateSumParallelWithThreads(int[] numbers)
    {
        long result = 0;
        List<Thread> threads = new List<Thread>();
        int numThreads = Environment.ProcessorCount;
        int chunkSize = numbers.Length / numThreads;

        for (int i = 0; i < numThreads; i++)
        {
            int start = i * chunkSize;
            int end = (i == numThreads - 1) ? numbers.Length : (i + 1) * chunkSize;
            int localStart = start;
            int localEnd = end;

            threads.Add(new Thread(() =>
            {
                long localSum = 0;
                for (int j = localStart; j < localEnd; j++)
                {
                    localSum += numbers[j];
                }
                Interlocked.Add(ref result, localSum);
            }));
        }

        foreach (Thread t in threads)
        {
            t.Start();
        }

        foreach (Thread t in threads)
        {
            t.Join();
        }

        return result;
    }


    // Параллельное вычисление суммы элементов массива с помощью ParalellForEach
    static long CalculateSumParallelWithParalellForEach(int[] numbers)
    {
        long result = 0;
        Parallel.ForEach(Partitioner.Create(0, numbers.Length), range =>
        {
            long localSum = 0;
            for (int i = range.Item1; i < range.Item2; i++)
            {
                localSum += numbers[i];
            }
            Interlocked.Add(ref result, localSum);
        });
        return result;
    }


    // Параллельное вычисление суммы элементов массива с помощью LINQ
    static long CalculateSumParallelWithLinq(int[] numbers)
    {
        return numbers.AsParallel().Sum(i => (long)i);
    }

    // Замер времени выполнения метода
    static void MeasureTime(Action action, string actionName)
    {
        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();

        Console.WriteLine($"{actionName}: Время выполнения - {sw.ElapsedMilliseconds} мс");
    }
}