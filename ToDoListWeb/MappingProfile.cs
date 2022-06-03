using AutoMapper;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Models;

namespace ToDoListWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkTask, WorkTaskApiResponse>()
            .ReverseMap();

            CreateMap<WorkTaskModel, WorkTask>();

            CreateMap<Status, StatusApiResponse>();

            CreateMap<TaskBoard, TaskBoardModel>()
            .ReverseMap();

            CreateMap<TaskBoard, TaskBoardApiResponse>()
            .ReverseMap();

            CreateMap<Priority, PriorityModel>()
            .ReverseMap();

            CreateMap<Priority, PriorityApiResponse>()
            .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id));        //Do poprawy nie mappuje Id z BaseEntity (Przy zwracaniu z bazy na Apiresponse)

            CreateMap<UserSignUpModel, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

            CreateMap<User, UserModel>();

            CreateMap<User, UserApiResponse>();

        }
    }
}
