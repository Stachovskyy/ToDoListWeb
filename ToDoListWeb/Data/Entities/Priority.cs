using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Data.Entities
{
    public class Priority : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<WorkTask> Tasks { get; set; }
    }
}
