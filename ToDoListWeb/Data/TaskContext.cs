using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public class TaskContext : DbContext
    {
        public DbSet<WorkTask> Tasks {get;set;}
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TaskBoard> TaskBoards{ get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
    }
}
