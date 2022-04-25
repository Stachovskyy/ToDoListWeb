using AutoMapper;
using ToDoListWeb.Data;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Entities;
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

            CreateMap<TaskBoard, TaskBoardModel>()
            .ReverseMap();

            CreateMap<TaskBoard, TaskBoardModelWithoutList>()
            .ReverseMap();

            CreateMap<Priority, PriorityModel>()
            .ReverseMap()
            .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<UserSignUp, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

            CreateMap<User, UserModel>();
        }
    }
}
