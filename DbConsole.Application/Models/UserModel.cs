
namespace DbConsole.Application.Models
{
    public class UserModel
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

        public static UserModel ConvertToApplicationModel(DbConsole.Infrastructure.Entities.User user)
        {
            return new UserModel { UserId = user.UserId, Name = user.Name, IsLecturer = user.IsLecturer };
        }

        public override string ToString()
        {
            return $"Пользователь с Id = '{UserId}', Именем = '{Name}', Признак преподавателя = '{IsLecturer}'";
        }
    }
}
