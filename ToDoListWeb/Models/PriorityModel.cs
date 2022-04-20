using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class PriorityModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}