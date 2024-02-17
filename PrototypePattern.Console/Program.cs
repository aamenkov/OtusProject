// See https://aka.ms/new-console-template for more information
using PrototypePattern.Console.Models;

internal class Program
{

    public static void Main(string[] args)
    {
        Person myPerson = new Person("Andrey", 24);

        // Клонирование через метод Clone() интерфейса ICloneable
        Person clonedPerson = (Person)myPerson.Clone();

        // Клонирование через метод Clone() реализованного интерфейса IMyCloneable
        Person myClonedPerson = myPerson.MyClone();
    }
}
