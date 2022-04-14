using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoListWeb.Data
{
    public class TaskRepository :ITaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }
        public async Task<List<WorkTask>> GetAll() 
        {
            var tasks = await _context.Tasks.Include(c => c.Status).Include(c => c.Size).ToListAsync();

            return tasks;
        }
    }
}
