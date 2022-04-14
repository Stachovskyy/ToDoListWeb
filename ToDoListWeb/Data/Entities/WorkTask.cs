using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public class WorkTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public DateTime FinishDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public Size Size { get; set; }
        public Status Status { get; set; }
    }
}
