using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListWeb.Data;

namespace ToDoListWeb.Models
{
    public class WorkTaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public SizeModel Size { get; set; }
        public StatusModel Status { get; set; }
    }
}
