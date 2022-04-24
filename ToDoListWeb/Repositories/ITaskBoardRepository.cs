using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Entities;

namespace ToDoListWeb.Data.Repositories
{
    public interface ITaskBoardRepository
    {
        Task<List<TaskBoard>> GetAllTaskBoardsAsync(User user);
        Task<TaskBoard> GetSingleTaskBoardAsync(User user, int taskBoardId);
        Task<TaskBoard> AddSingleTaskBoard(User user, TaskBoard taskBoard);
        Task SoftDelete(User user, int Id);
        Task<TaskBoard> AddTaskBoardToAnotherUser(User user, TaskBoard taskBoard);
        Task<TaskBoard> GetSingleTaskBoardAsync(int taskBoardId);
        Task<bool> SaveChangesAsync();
    }
}
