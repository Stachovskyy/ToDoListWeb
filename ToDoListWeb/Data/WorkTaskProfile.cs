using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ToDoListWeb.Models;

namespace ToDoListWeb.Data
{
    public class WorkTaskProfile : Profile
    {
        public WorkTaskProfile()
        {
            CreateMap<WorkTask, WorkTaskModel>();
            CreateMap<Size, SizeModel>();
            CreateMap<Status, StatusModel>();
            CreateMap<WorkTask, WorkTaskModel>();
        }
    }
}
