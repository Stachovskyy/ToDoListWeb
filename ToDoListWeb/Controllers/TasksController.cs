using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Data.Repositories;
using ToDoListWeb.Exceptions;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{

    [ApiController]
    [Route("api/TaskBoards/{taskBoardId}/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ITaskBoardRepository _taskBoardRepository;
        private readonly UserManager<User> _userManager;

        public TasksController(ITaskRepository taskRepository, IMapper mapper, ITaskBoardRepository taskBoardRepository, UserManager<User> userManager)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _taskBoardRepository = taskBoardRepository;
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<WorkTaskApiResponse>>> GetTasks(           
            int taskBoardId,
            [FromQuery] int? statusId = null,
            [FromQuery] int? priorityId = null,
            [FromQuery] int? take = null,
            [FromQuery] int? skip = null)
        {
            await ValidateTaskBoard(taskBoardId);   // Musze dodać walidacje tutaj ! czy uzytkownik ma dostep do danego borda 

            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var listOfTasks = await _taskRepository.GetTasks(statusId, priorityId, take, skip);

            return _mapper.Map<List<WorkTaskApiResponse>>(listOfTasks);
        }

        [HttpGet("{taskId:int}")]
        public async Task<ActionResult<WorkTaskApiResponse>> GetTask(
            int taskBoardId,
            int taskId)
        {
            await ValidateTaskBoard(taskBoardId); //Tutaj tak samo sprawdzic 

            var task = await _taskRepository.GetSingleAsync(taskId);

            if (task == null)
            {
                throw new NotFoundException("Could not found Task");
            }
            if (task.TaskBoardId != taskBoardId)
            {
                throw new NotFoundException("Could not found Task");
            }

            return _mapper.Map<WorkTaskApiResponse>(task);
        }
        
        [HttpPost]
        public async Task<ActionResult<WorkTaskApiResponse>> AddTask(
            [FromBody] WorkTaskModel model,   
            [System.Web.Http.FromUri] int taskBoardId)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = _mapper.Map<WorkTask>(model);

            var createdTask = await _taskRepository.AddAsync(task);

            var taskmodel = _mapper.Map<WorkTaskApiResponse>(createdTask);

            return StatusCode((int)HttpStatusCode.Created, taskmodel);
        }
        
        [HttpPut("{taskId:int}")]
        public async Task<ActionResult<WorkTaskApiResponse>> UpdateTask(   
            [System.Web.Http.FromUri] int taskBoardId,
            int taskId,
            WorkTaskModel model)
        {
            await ValidateTaskBoard(taskBoardId);

            var oldTask = await _taskRepository.GetSingleAsync(taskId);
            if (oldTask == null)
            {
                throw new NotFoundException("Could not found Task to update");
            }

            var updatedTask = _mapper.Map(model, oldTask);      
           
            await _taskRepository.SaveChangesAsync();

            var returnedTask = _mapper.Map<WorkTaskApiResponse>(updatedTask);

            return returnedTask;

        }
       
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteTask(
            [System.Web.Http.FromUri] int taskBoardId,
            int Id)
        {
            await ValidateTaskBoard(taskBoardId);

            var task = await _taskRepository.GetSingleAsync(Id);

            if (task == null)
            {
                return NotFound("Task does not exists");
            }

            await _taskRepository.SoftDelete(Id);

            return NoContent();
        }
        private async Task ValidateTaskBoard(
            int taskBoardId)
        {
            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(taskBoardId);

            if (taskBoard == null)
            {
                throw new NotFoundException("TaskBoard does not exists");
            }
        }
    }
}

