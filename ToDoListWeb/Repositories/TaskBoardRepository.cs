using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Entities;
using ToDoListWeb.Exceptions;

namespace ToDoListWeb.Data.Repositories
{
    public class TaskBoardRepository : ITaskBoardRepository
    {
        private readonly MainContext _context;

        public TaskBoardRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<List<TaskBoard>> GetAllTaskBoardsAsync(User user)
        {
            var taskBoards = await _context.TaskBoards
                .Include(t => t.UserList)
                .Where(t => t.UserList
                            .Contains(user) & t.IsDeleted != true)
                            .ToListAsync();

            return taskBoards;
        }

        public async Task<TaskBoard> GetSingleTaskBoardAsync(User user, int taskBoardId)
        {
            var taskBoards = await _context.TaskBoards.
                Include(t => t.UserList)
                .Where(t => t.UserList.
                            Contains(user) & t.Id == taskBoardId & t.IsDeleted != true)
                            .SingleOrDefaultAsync();

            return taskBoards;
        }

        public async Task<TaskBoard> AddSingleTaskBoard(User user, TaskBoard taskBoard)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var addedTaskBoard = await _context.TaskBoards.AddAsync(taskBoard);

            await _context.SaveChangesAsync();

            var newtaskBoard = await GetSingleTaskBoardAsync(addedTaskBoard.Entity.Id);

            newtaskBoard.UserList.Add(user);
            throw new Exception();

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return newtaskBoard;

        }

        public async Task<TaskBoard> AddTaskBoardToAnotherUser(User user, TaskBoard taskBoard)
        {
            taskBoard.UserList.Add(user);

            await _context.SaveChangesAsync();

            var task = await GetSingleTaskBoardAsync(taskBoard.Id);

            return task;
        }
        public async Task SoftDelete(User user, int Id)
        {
            var taskToDelete = await GetSingleTaskBoardAsync(user, Id);

            if (taskToDelete == null)
                throw new NotFoundException("There is no taskBoards to delete");

            taskToDelete.IsDeleted = true;

            await _context.SaveChangesAsync();

        }

        public async Task<TaskBoard> GetSingleTaskBoardAsync(int taskBoardId)
        {
            var taskBoard = await _context.TaskBoards
                .Where(t => t.Id == taskBoardId)
                .SingleOrDefaultAsync();

            return taskBoard;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
