using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class TaskBoardModel
    {
        [Required(ErrorMessage = ("Taskboard name is required"))]
        public string Name { get; set; }
        
    }
}
