using PrototypePattern.Console.Interfaces;

namespace PrototypePattern.Console.Models
{
    public class Student : Person, IMyCloneable<Student>, ICloneable
    {
        public string School { get; set; }

        public Student(string name, int age, string school)
            : base(name, age)
        {
            School = school;
        }

        public override Student MyClone()
        {
            return new Student(base.Name, base.Age, School);
        }

        public object Clone()
        {
            return this.Clone();
        }
    }

}
