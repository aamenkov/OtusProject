using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Console.Models
{
    public class ExampleClass
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public ExampleClass()
        {

        }

        public ExampleClass(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
