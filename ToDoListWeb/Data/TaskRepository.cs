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
        private readonly MainContext _context;

        public TaskRepository(MainContext context)
        {
            _context = context;
        }
        public async Task<List<WorkTask>> GetAllAsync() 
        {
            var tasks = await _context.Tasks.Include(c => c.Status).Include(c => c.Size).ToListAsync();

            return tasks;

        }
        public async Task<WorkTask> AddAsync(WorkTask task)
        {
            var createdTask=await _context.Tasks.AddAsync(task);
            _context.SaveChanges();
            return createdTask.Entity;
        }

        public async Task<WorkTask> GetSingleAsync(int Id)
        {
            var task = await _context.Tasks.Include(c => c.Status).Include(c => c.Size).Where(c => c.Id == Id).FirstOrDefaultAsync();

            return task;
        }

        public void Delete(WorkTask entity)       //czy to tez powinno byc async ?
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
