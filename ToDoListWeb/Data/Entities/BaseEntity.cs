namespace ToDoListWeb.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }  //Chyba bez tego powinno byc ??
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
