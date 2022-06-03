namespace ToDoListWeb.Models
{
    public class WorkTaskApiResponse
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public StatusApiResponse? Status { get; set; }
        public PriorityModel Priority { get; set; }
    }
}
