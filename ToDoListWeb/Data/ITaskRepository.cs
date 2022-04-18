using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public interface ITaskRepository
    {
        Task<List<WorkTask>> GetAllAsync();
        Task<WorkTask> AddAsync(WorkTask workTask);
        Task<WorkTask> GetSingleAsync(int Id);
        void Delete(WorkTask workTask);
    }
}
