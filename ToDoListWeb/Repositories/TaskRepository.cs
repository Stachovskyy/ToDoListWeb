using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Data.Repositories;

namespace ToDoListWeb.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MainContext _context;

        public TaskRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<WorkTask> AddAsync(WorkTask task)
        {
            var createdTask = await _context.Tasks
                .AddAsync(task);

            await _context.SaveChangesAsync();

            return createdTask.Entity;

        }

        public async Task<WorkTask> GetSingleAsync(int Id)
        {
            var task = await _context.Tasks
                .Include(c => c.Status)
                .Include(c => c.Priority)
                .Where(c => c.Id == Id && c.IsDeleted != true)
                .FirstOrDefaultAsync();

            return task;
        }

        public async Task SoftDelete(int taskId)
        {
            var taskToDelete = await GetSingleAsync(taskId);

            taskToDelete.IsDeleted = true;

            await _context.SaveChangesAsync();

        }

        public async Task<List<WorkTask>> GetTasks(int? statusId, int? priorityId, int? take = null, int? skip = null)
        {
            var tasks = _context.Tasks
                .Include(c => c.Status)
                .Include(c => c.Priority)
                .Where(c => c.IsDeleted != true);

            if (statusId != null)
            {
                tasks = tasks.Where(s => s.StatusId == statusId);
            }

            if (priorityId != null)
            {
                tasks = tasks.Where(s => s.PriorityId == priorityId);
            }

            tasks = tasks.OrderBy(c => c.Id);

            if (skip != null)
            {
                tasks = tasks.Skip(skip.Value);
            }
            if (take != null)
            {
                tasks = tasks.Take(take.Value);
            }

            return await tasks.ToListAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
