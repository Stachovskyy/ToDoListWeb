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

            CreateMap<WorkTaskCreateModel, WorkTask>();

            CreateMap<Status, StatusModel>();

            CreateMap<TaskBoard, TaskBoardModel>();

            CreateMap<Priority, PriorityModel>()
            .ReverseMap();
        }
    }
}
