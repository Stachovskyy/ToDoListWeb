using System.ComponentModel.DataAnnotations;


namespace ToDoListWeb.Data.Entities
{
    public class WorkTask : BaseEntity   
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public Priority Priority { get; set; }
        [Required]
        public int PriorityId { get; set; }
        [Required]
        public int TaskBoardId { get; set; }
    }
}
