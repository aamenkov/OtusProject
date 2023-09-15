namespace DbConsole.Infrastructure.Entities
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class User
    {
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Признак преподавателя
        /// </summary>
        public bool IsLecturer { get; set; }

        public virtual ICollection<UserGrade> UserGrades { get; set; }
    }
}
