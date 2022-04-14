using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public interface ITaskRepository
    {
        Task<List<WorkTask>> GetAll();
    }
}
