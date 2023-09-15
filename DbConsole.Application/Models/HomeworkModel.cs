
using DbConsole.Infrastructure.Entities;
using System.Xml.Linq;

namespace DbConsole.Application.Models
{
    public class HomeworkModel
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

        public HomeworkModel ConvertToApplicationModel(DbConsole.Infrastructure.Entities.Homework homework)
        {
            return new HomeworkModel { Id = homework.Id, Description = homework.Description, Title = homework.Title};
        }

        public override string ToString()
        {
            return $"ДЗ с Id = '{Id}', Названием = '{Title}', Описанием = '{Description}'";
        }
    }
}
