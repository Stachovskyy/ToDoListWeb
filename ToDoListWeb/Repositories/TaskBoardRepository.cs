using Microsoft.EntityFrameworkCore;

namespace ToDoListWeb.Data
{
    public class TaskBoardRepository : ITaskBoardRepository
    {
        private readonly MainContext _context;

        public TaskBoardRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<List<WorkTask>> GetAllAsync(int taskBoardId)
        {
            var tasks = await _context.Tasks
                .Include(c => c.Status)
                .Include(c => c.Priority)
                .Where(c => c.TaskBoardId == taskBoardId)
                .ToListAsync();

            return tasks;

        }
        public async Task<TaskBoard> GetSingleTaskBoardAsync(int taskBoardId)
        {
            var taskBoard = await _context.TaskBoards
                .Where(t => t.Id == taskBoardId)
                .SingleOrDefaultAsync();

            return taskBoard;

        }
    }
}
