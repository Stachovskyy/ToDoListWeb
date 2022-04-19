using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Exceptions;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    [ApiController]
    [Route("api/TaskBoards/{taskBoardId}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ITaskBoardRepository _taskBoardRepository;

        public TasksController(ITaskRepository taskRepository, IMapper mapper, ITaskBoardRepository taskBoardRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _taskBoardRepository = taskBoardRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkTaskResponse>>> GetTasks(
            int taskBoardId,
            [FromQuery] int? statusId = null,
            [FromQuery] int? priorityId = null)
        {
            await ValidateTaskBoard(taskBoardId);

            var listOfTasks = await _taskRepository.GetTasks(statusId, priorityId);

            if (!listOfTasks.Any())
                throw new NotFoundException("Could not find any tasks in this particualr taskboard");

            return _mapper.Map<List<WorkTaskResponse>>(listOfTasks);
        }

        [HttpGet("{taskId:int}")]
        public async Task<ActionResult<WorkTaskResponse>> GetSingleTask(int taskBoardId, int taskId)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = await _taskRepository.GetSingleAsync(taskId);

            if (task == null)
                throw new NotFoundException("Could not found Task");

            if (task.TaskBoardId != taskBoardId)
                throw new NotFoundException("Could not found Task");

            return _mapper.Map<WorkTaskResponse>(task);
        }

        [HttpPost]
        public async Task<ActionResult<WorkTaskResponse>> AddTask([FromBody] WorkTaskCreateModel model, [System.Web.Http.FromUri] int taskBoardId)   //Tutaj bez id TaskBoarda / bo mam go w urlu
        {
            await ValidateTaskBoard(taskBoardId);

            var task = _mapper.Map<WorkTask>(model);

            var createdTask = await _taskRepository.AddAsync(task);

            var taskmodel = _mapper.Map<WorkTaskResponse>(createdTask);

            return StatusCode((int)HttpStatusCode.Created, taskmodel);
        }
        [HttpPut("{taskId:int}")]
        public async Task<ActionResult<WorkTaskResponse>> Put([System.Web.Http.FromUri] int taskBoardId, int taskId, WorkTaskCreateModel model) //wszystko co mozliwe do edycji
        {
            await ValidateTaskBoard(taskBoardId);

            var oldTask = await _taskRepository.GetSingleAsync(taskId);

            var updatedTask = _mapper.Map(model, oldTask);

            var returnedTask = _mapper.Map<WorkTaskResponse>(updatedTask);  

            return returnedTask;
        }
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete([System.Web.Http.FromUri] int taskBoardId, int Id)  // zmienic zeby softdelete isdeleted (zmienic przy pobieraniu taskow)
        {
            await ValidateTaskBoard(taskBoardId);
            var task = await _taskRepository.GetSingleAsync(Id);
            if (task == null) return NotFound("Task does not exists");
            await _taskRepository.Delete(Id);            //czy jest mozliwe zeby task nie istnial i byl do sprawdzenia ?
            return NoContent();
        }
        private async Task ValidateTaskBoard(int taskBoardId)
        {
            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(taskBoardId);
            if (taskBoard == null)
                throw new NotFoundException("TaskBoard does not exists");
        }
    }
}

