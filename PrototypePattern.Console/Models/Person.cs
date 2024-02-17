using PrototypePattern.Console.Interfaces;

namespace PrototypePattern.Console.Models
{
    public class Person : IMyCloneable<Person>, ICloneable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual Person MyClone()
        {
            return new Person(Name, Age);
        }

        public object Clone()  
        {
            return this.Clone();
        }
    }
}
