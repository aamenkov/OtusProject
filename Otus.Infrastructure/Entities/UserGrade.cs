using System.Data;

namespace Otus.Infrastructure.Entities
{
    /// <summary>
    /// Оценки ученика
    /// </summary>
    public class UserGrade
    {
        public long Id { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Комментарий 
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Признак прохождения задания 
        /// </summary>
        public bool IsPassed { get; set; }

        public virtual Homework Homework { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
