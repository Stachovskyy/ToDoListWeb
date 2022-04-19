using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Data
{
    public class Priority : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<WorkTask> Tasks { get; set; }
    }
}
