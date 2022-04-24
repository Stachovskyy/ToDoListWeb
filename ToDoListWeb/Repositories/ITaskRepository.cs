using ToDoListWeb.Data.Entities;

namespace ToDoListWeb.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<WorkTask> AddAsync(WorkTask workTask);
        Task<WorkTask> GetSingleAsync(int Id);
        Task Delete(int taskId);
        Task<List<WorkTask>> GetTasks(int? statusId, int? priorityId,int? take = null, int? skip = null);
        Task<bool> SaveChangesAsync();
    }
}
