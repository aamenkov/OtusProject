namespace Otus.DbConsole.Infrastructure.Entities
{
    /// <summary>
    /// Домашнее задание для учеников
    /// </summary>
    public class Homework
    {
        public long Id { get; set; }

        /// <summary>
        /// Заголовок задания 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание задания 
        /// </summary>
        public string? Description { get; set; }

        public virtual ICollection<UserGrade> UserGrades { get; set; }
    }
}
