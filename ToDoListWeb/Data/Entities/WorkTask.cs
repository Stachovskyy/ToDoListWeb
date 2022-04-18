using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWeb.Data
{
    public class WorkTask        //zapytac czemu mam TaskBoardId  bazie danych
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }

    }
}
