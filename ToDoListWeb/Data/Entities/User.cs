using Microsoft.AspNetCore.Identity;
using ToDoListWeb.Data.Entities;

namespace ToDoListWeb.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TaskBoard> TaskBoards { get; set; } = new List<TaskBoard>();
    }
}
