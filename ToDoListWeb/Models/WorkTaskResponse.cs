namespace ToDoListWeb.Models
{
    public class WorkTaskResponse
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public StatusModel? Status { get; set; }
        public PriorityModel Priority { get; set; }
    }
}
