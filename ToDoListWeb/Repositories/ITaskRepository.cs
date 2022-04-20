namespace ToDoListWeb.Data
{
    public interface ITaskRepository
    {
        Task<WorkTask> AddAsync(WorkTask workTask);
        Task<WorkTask> GetSingleAsync(int Id);
        Task Delete(int taskId);
        Task<List<WorkTask>> GetTasks(int? statusId, int? priorityId,int? take = null, int? skip = null);
        /*Task<WorkTask> UpdateAsync(WorkTask model, int id);*/
        Task<bool> SaveChangesAsync();
    }
}
