using PrototypePattern.Console.Interfaces;

namespace PrototypePattern.Console.Models
{
    public class Teacher : Person, IMyCloneable<Teacher>, ICloneable
    {
        public string Subject { get; set; }

        public Teacher(string name, int age, string subject)
            : base(name, age)
        {
            Subject = subject;
        }

        public override Teacher MyClone()
        {
            return new Teacher(base.Name, base.Age, Subject);
        }

        public object Clone()
        {
            return this.Clone();
        }
    }

}
