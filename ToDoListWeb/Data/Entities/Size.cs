namespace ToDoListWeb.Data
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WorkTask> Tasks { get; set; }
    }
}
