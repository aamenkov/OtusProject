namespace Otus.Infrastructure.Entities
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Признак преподавателя
        /// </summary>
        public bool IsLecturer { get; set; }
    }
}
