namespace ToDoListWeb.Data
{
    public interface ITaskBoardRepository
    {
        Task<List<WorkTask>> GetAllAsync(int taskBoardId);
        Task<TaskBoard> GetSingleTaskBoardAsync(int taskBoardId);
    }
}
