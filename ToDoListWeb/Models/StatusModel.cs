using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WorkTaskModel> Tasks { get; set; }
    }
}
