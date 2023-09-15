namespace DbConsole.Infrastructure.Entities
{
    /// <summary>
    /// Оценки ученика
    /// </summary>
    public class UserGrade
    {
        public long UserGradeId { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Комментарий 
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Признак прохождения задания 
        /// </summary>
        public bool IsPassed { get; set; }

        /// <summary>
        /// Дата создания оценки
        /// </summary>
        public DateTime DateTimeCreated { get; set; }

        public long HomeworkId { get; set; }
        public virtual Homework Homework { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
