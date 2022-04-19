using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class PriorityModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}