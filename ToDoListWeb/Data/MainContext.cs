using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Entities;

namespace ToDoListWeb.Data
{
    public class MainContext : IdentityDbContext<User, Role, Guid>           //Guid to Typ Id dla User i Role
    {
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  //

            modelBuilder.Entity<Status>()
                .HasData(
                        new Status { Id = 1, Name = "Big" },
                        new Status { Id = 2, Name = "Medium" },
                        new Status { Id = 3, Name = "Small" });

            modelBuilder.Entity<Priority>()
                .HasData(
                        new Priority { Id = 1, Name = "Important" });

            modelBuilder.Entity<TaskBoard>()
                .HasData(
                        new TaskBoard { Id = 1, Name = "Moj taskboard", });
           }
    }
}
