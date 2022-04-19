using Microsoft.EntityFrameworkCore;

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

            _context.SaveChanges();

            return createdTask.Entity;

        }

        public async Task<WorkTask> GetSingleAsync(int Id)
        {
            var task = await _context.Tasks
                .Include(c => c.Status)
                .Include(c => c.Priority)
                .Where(c => c.Id == Id & c.IsDeleted != true)
                .FirstOrDefaultAsync();

            return task;
        }

        public async Task Delete(int taskId)
        {
            var taskToDelete = await GetSingleAsync(taskId);

            taskToDelete.IsDeleted = true;

            _context.SaveChanges();

        }

        public async Task<List<WorkTask>> GetTasks(int? statusId, int? priorityId)
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

            return await tasks.ToListAsync();

        }
    }
}
