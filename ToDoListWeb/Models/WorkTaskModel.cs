using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Models
{
    public class WorkTaskModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        [Required]
        public int? StatusId { get; set; }
        [Required(ErrorMessage = "PriorityId is required")]
        public int? PriorityId { get; set; }
        [Required]
        public int TaskBoardId { get; set; }
    }
}

