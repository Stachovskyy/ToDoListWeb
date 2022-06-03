using ToDoListWeb.Data.Entities;
using ToDoListWeb.Models;

namespace ToDoListWeb.Data.Repositories
{
    public interface ITaskBoardRepository
    {
        Task<List<TaskBoard>> GetTaskBoardsAssignedToUserAsync(User user);
        Task<TaskBoard> GetSingleTaskBoardAsync(User user, int taskBoardId);
        Task<TaskBoard> AddSingleTaskBoard(User user, TaskBoard taskBoard);
        Task SoftDelete(User user, int Id);
        Task<TaskBoard> AssignUserToTaskBoard(User user, TaskBoard taskBoard);
        Task<TaskBoard> GetSingleTaskBoardAsync(int taskBoardId);
        Task<bool> SaveChangesAsync();
    }
}
