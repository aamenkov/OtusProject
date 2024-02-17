// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Reflection.Console.Models;
using System.Diagnostics;

internal class Program
{
    public static void Main(string[] args)
    {
        F obj = new F().Get();
        var amount = 100000;

        Console.WriteLine($"Elapsed Time (amount = {amount})");
        Console.WriteLine();

        MeasureCustomSerialization(obj, amount);
        MeasureStandardSerialization(obj, amount);
        MeasureCustomDeserialization(obj, amount);
        MeasureStandardDeserialization(obj, amount);

        Console.WriteLine();

        ExampleClass exampleObject = new ExampleClass("John", 30);

        MeasureCustomCsvSerialization(exampleObject);
        MeasureCustomCsvDeserialization();
    }


    /// <summary>
    /// Кастомная сериализация
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="amount"></param>
    private static void MeasureCustomSerialization(F obj, int amount)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < amount; i++)
        {
            string serializedString = FConverter.SerializeFToString(obj);
        }
        sw.Stop();
        Console.WriteLine($"For Custom Serialization: {sw.Elapsed}");
    }


    /// <summary>
    /// Сериализация с помощью Newtonsoft
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="amount"></param>
    private static void MeasureStandardSerialization(F obj, int amount)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < amount; i++)
        {
            string jsonSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        sw.Stop();
        Console.WriteLine($"For JSON Serialization: {sw.Elapsed}");
    }


    /// <summary>
    /// Кастомная десериализация 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="amount"></param>
    private static void MeasureCustomDeserialization(F obj, int amount)
    {
        var myFSerialized = FConverter.SerializeFToString(obj);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < amount; i++)
        {
            var FDeserialized = FConverter.DeserializeFFromString(myFSerialized);
        }
        sw.Stop();
        Console.WriteLine($"For Custom Deserialization: {sw.Elapsed}");
    }


    /// <summary>
    /// Десериализация с помощью Newtonsoft
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="amount"></param>
    private static void MeasureStandardDeserialization(F obj, int amount)
    {
        var myJsonSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < amount; i++)
        {
            var jsonDeserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<F>(myJsonSerialized);
        }
        sw.Stop();
        Console.WriteLine($"For JSON Deserialization: {sw.Elapsed}");
    }


    /// <summary>
    /// Кастомная сериализация csv
    /// </summary>
    /// <param name="exampleObject"></param>
    private static void MeasureCustomCsvSerialization(ExampleClass exampleObject)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string csvData = CsvSerializer.SerializeToCsv(exampleObject);
        sw.Stop();
        Console.WriteLine($"For CSV Custom Serialization: {sw.Elapsed}");
        File.WriteAllText("example.csv", csvData);
    }


    /// <summary>
    /// Кастомная десериализация csv
    /// </summary>
    private static void MeasureCustomCsvDeserialization()
    {
        string csvDataFromFile = File.ReadAllText("example.csv");
        Stopwatch sw = new Stopwatch();
        sw.Start();
        ExampleClass deserializedObject = CsvSerializer.DeserializeFromCsv<ExampleClass>(csvDataFromFile);
        sw.Stop();
        Console.WriteLine($"For CSV Custom Deserialization: {sw.Elapsed}");
        Console.WriteLine($"Name: {deserializedObject.Name}, Age: {deserializedObject.Age}");
    }
}