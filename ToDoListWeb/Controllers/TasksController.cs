using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Data.Repositories;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<WorkTaskResponse>>> GetTasks(           //zeby skorzystac z metody musze przekazac token
            int taskBoardId,
            [FromQuery] int? statusId = null,
            [FromQuery] int? priorityId = null,
            [FromQuery] int? take = null,
            [FromQuery] int? skip = null)
        {
            await ValidateTaskBoard(taskBoardId);

            var listOfTasks = await _taskRepository.GetTasks(statusId, priorityId, take, skip);

            if (!listOfTasks.Any())
                throw new NotFoundException("Could not find any tasks in this particualr taskboard");

            return _mapper.Map<List<WorkTaskResponse>>(listOfTasks);
        }

        [Authorize]
        [HttpGet("{taskId:int}")]
        public async Task<ActionResult<WorkTaskResponse>> GetSingleTask(
            int taskBoardId,
            int taskId)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = await _taskRepository.GetSingleAsync(taskId);

            if (task == null)
                throw new NotFoundException("Could not found Task");

            if (task.TaskBoardId != taskBoardId)
                throw new NotFoundException("Could not found Task");

            return _mapper.Map<WorkTaskResponse>(task);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WorkTaskResponse>> AddTask(
            [FromBody] WorkTaskCreateModel model,
            [System.Web.Http.FromUri] int taskBoardId)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = _mapper.Map<WorkTask>(model);

            var createdTask = await _taskRepository.AddAsync(task);

            var taskmodel = _mapper.Map<WorkTaskResponse>(createdTask);

            return StatusCode((int)HttpStatusCode.Created, taskmodel);
        }
        [Authorize]
        [HttpPut("{taskId:int}")]
        public async Task<ActionResult<WorkTaskResponse>> Put(
            [System.Web.Http.FromUri] int taskBoardId,
            int taskId,
            WorkTaskCreateModel model)
        {
            await ValidateTaskBoard(taskBoardId);

            var oldTask = await _taskRepository.GetSingleAsync(taskId);
            if (oldTask == null)
                throw new NotFoundException("Could not found Task to update");

            var updatedTask = _mapper.Map(model, oldTask);      //Mapuje

            await _taskRepository.SaveChangesAsync();

            var returnedTask = _mapper.Map<WorkTaskResponse>(updatedTask);

            return returnedTask;

        }
        [Authorize]
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(
            [System.Web.Http.FromUri] int taskBoardId,
            int Id)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = await _taskRepository.GetSingleAsync(Id);

            if (task == null)
                return NotFound("Task does not exists");

            await _taskRepository.Delete(Id);

            return NoContent();
        }
        private async Task ValidateTaskBoard(
            int taskBoardId)
        {
            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(taskBoardId);

            if (taskBoard == null)
                throw new NotFoundException("TaskBoard does not exists");
        }
    }
}

