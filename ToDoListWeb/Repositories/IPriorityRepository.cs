using ToDoListWeb.Data.Entities;

namespace ToDoListWeb.Data.Repositories
{
    public interface IPriorityRepository
    {
        public Task<Priority> AddPriority(Priority priority);
        public Task<List<Priority>> GetPrioritiesAsync();
        public Task<Priority> GetPriority(int priorityId);
        public Task SoftDelete(int priorityId);
        Task<Priority> GetPriorityByName(string Name);
        Task<bool> SaveChangesAsync();
    }
}