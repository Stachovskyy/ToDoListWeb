using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ToDoListWeb.Data;
using ToDoListWeb.Models;

namespace ToDoListWeb
{
    public class WorkTaskProfile : Profile
    {
        public WorkTaskProfile()
        {
            CreateMap<WorkTask, WorkTaskResponse>()
            .ReverseMap();

            CreateMap<WorkTaskCreateModel, WorkTask>()
            .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Size, SizeModel>();

            CreateMap<Status, StatusModel>();

            CreateMap<TaskBoard, TaskBoardModel>();
        }
    }
}
