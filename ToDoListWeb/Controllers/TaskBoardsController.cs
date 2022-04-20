using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Exceptions;

namespace ToDoListWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBoardsController : ControllerBase
    {
        private readonly ITaskBoardRepository _taskBoardRepository;
        private readonly IMapper _mapper;

        public TaskBoardsController(ITaskBoardRepository taskBoardRepository, IMapper mapper)
        {
            _taskBoardRepository = taskBoardRepository;
            _mapper = mapper;
        }
        //[HttpGet("{taskBoardId}")]
        //public async Task<ActionResult<List<WorkTaskResponse>>> GetAllTasks(int taskBoardId,[FromQuery]int? statusId=null,[FromQuery] int? priorityId=null)         //ZAPYTAC JAK NAPISAC ZEBY DALO SIE WZIAC TYLKO DUZE albo TYLKO DUZE I WAŻNE albo DUŻE WAŻNE I W TRAKCIE ... ? ale zeby tez dalo sie wszystkie
        //{
        //    ValidateTaskBoard(taskBoardId);

        //    if (statusId == null && priorityId == null)
        //    { 
        //      await _taskBoardRepository.GetAllAsync(taskBoardId);
        //    }


        //    if (!tasks.Any()) return NotFound("Could not find any tasks in this particualr taskboard"); // ZAPYTAC CZY TO MA BYC TAK CZY Throw czy jak ? czy moze w try getcie calosc
        //    var tasksModel = _mapper.Map<List<WorkTaskResponse>>(tasks);
        //    return tasksModel;
        //}

        //[HttpGet("{taskBoardId/search")]   //Searchstring dla roznych statusów

        //public async Task<ActionResult<List<WorkTaskResponse>>> GetAllTasksQuried(int taskBoardId,)
        //{

        //}


        ////Tworzenie custom statusu priority czy rozmiaru w osobnym kontrolerze dla tego entity
        ////
        private async Task ValidateTaskBoard(int taskBoardId)
        {
            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(taskBoardId);
            if (taskBoard == null)
                throw new NotFoundException("TaskBoard does not exists");
        }
    }
}
