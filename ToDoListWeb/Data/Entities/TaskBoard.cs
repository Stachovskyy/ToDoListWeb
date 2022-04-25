using System.ComponentModel.DataAnnotations;
using ToDoListWeb.Entities;

namespace ToDoListWeb.Data.Entities
{
    public class TaskBoard : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<WorkTask>? TaskList { get; set; }
        public List<User> UserList { get; set; } = new List<User>(); 
    }
}
