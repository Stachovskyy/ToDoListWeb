using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class StatusModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
