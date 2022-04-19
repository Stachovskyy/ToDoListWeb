using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class TaskBoardModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = ("Taskboard name is required"))]
        public string Name { get; set; }
        public List<WorkTaskResponse> List { get; set; }
    }
}
