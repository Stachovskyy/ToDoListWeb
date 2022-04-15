using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public class MainContext : DbContext
    {
        public DbSet<WorkTask> Tasks {get;set;}
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TaskBoard> TaskBoards{ get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
    }
}
