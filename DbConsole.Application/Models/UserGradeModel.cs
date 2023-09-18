
using System.Xml.Linq;

namespace DbConsole.Application.Models
{
    public class UserGradeModel
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

        public Guid UserId { get; set; }

        public static UserGradeModel ConvertToApplicationModel(DbConsole.Infrastructure.Entities.UserGrade grade)
        {
            return new UserGradeModel 
            { 
                UserGradeId = grade.UserGradeId, 
                UserId = grade.UserId, 
                Comment = grade.Comment, 
                DateTimeCreated = grade.DateTimeCreated,
                HomeworkId = grade.HomeworkId,
                IsPassed = grade.IsPassed,
                Quantity = grade.Quantity
            };
        }

        public override string ToString()
        {
            return $"Оценка домашнего задания с " +
                $"UserGradeId = '{UserGradeId}', " +
                $"дз с HomeworkId = '{HomeworkId}', " +
                $"пользователя с UserId = '{UserId}', " +
                $"значение оценки = '{Quantity}', " +
                $"комментарий = '{Comment}', " +
                $"прохождение дз = '{IsPassed}', " +
                $"время создания = '{DateTimeCreated}'";
        }
    }
}
