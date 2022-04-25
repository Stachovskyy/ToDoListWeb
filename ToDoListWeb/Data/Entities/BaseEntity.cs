namespace ToDoListWeb.Data
{
    public class BaseEntity
    {
        public int Id { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
