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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskBoardsController : ControllerBase
    {
        private readonly ITaskBoardRepository _taskBoardRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public TaskBoardsController(ITaskBoardRepository taskBoardRepository, IMapper mapper, UserManager<User> userManager)
        {
            _taskBoardRepository = taskBoardRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskBoardApiResponse>>> GetTaskBoards()
        {
            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoards = await _taskBoardRepository.GetTaskBoardsAssignedToUserAsync(user);

            taskBoards = taskBoards.Where(t => t.UserList
                              .Contains(user)).ToList();

            var taskBoardToTeturn = _mapper.Map<List<TaskBoardApiResponse>>(taskBoards);
            return taskBoardToTeturn;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskBoardApiResponse>> GetTaskBoard(int id)
        {
            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(id);

            if(!taskBoard.UserList.Contains(user))
            {
                return Ok();
            }

                var taskBoardToTeturn = _mapper.Map<TaskBoardApiResponse>(taskBoard);
                return taskBoardToTeturn;
        }

        [HttpPost] 
        public async Task<ActionResult<TaskBoardModel>> CreateTaskBoard([FromBody] TaskBoardModel model)
        {
            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = _mapper.Map<TaskBoard>(model);

            var addedTaskBoard = await _taskBoardRepository.AddSingleTaskBoard(user, taskBoard);  //Przerobic ta metodę (Rozbić na dwie metody o jednej funkcjonalnosci)

            return _mapper.Map<TaskBoardModel>(addedTaskBoard);
        }

        [HttpPut("{Id:int}")]  //Przerobic ja tez
        public async Task<ActionResult<TaskBoardModel>> AddUserToTaskBoard(string user, TaskBoardModel model, [System.Web.Http.FromUri] int Id)
        {
            var loggedUser = User.Identity.Name;

            var mainUser = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(mainUser, Id);
            if (taskBoard == null)
            {
                throw new NotFoundException("There is no TaskBoard with this Id");
            }

            taskBoard = _mapper.Map(model, taskBoard);

            var wholeUser = await _userManager.FindByNameAsync(user);

            if (wholeUser == null)
            {
                throw new NotFoundException("There is no user with this username");
            }

            var updatedtask = await _taskBoardRepository.AssignUserToTaskBoard(wholeUser, taskBoard);

            return _mapper.Map<TaskBoardModel>(updatedtask);
        }

        [HttpPatch("{Id:int}")] 
        public async Task<ActionResult<TaskBoardApiResponse>> UpdateBoard(TaskBoardModel model, [System.Web.Http.FromUri] int Id)
        {
            var loggedUser = User.Identity.Name;

            var mainUser = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(mainUser, Id); // Ja też zmienić pobrać borda po id i sprawdzic czy user ma dostep tak jak zrobiłem to wyzej !
            if (taskBoard == null)
            {
                throw new NotFoundException("There is no TaskBoard with this Id");
            }

            taskBoard = _mapper.Map(model, taskBoard);

            await _taskBoardRepository.SaveChangesAsync();


            return _mapper.Map<TaskBoardApiResponse>(taskBoard);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteTaskBoard(int Id)
        {
            var loggeduser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggeduser);

            await _taskBoardRepository.SoftDelete(user, Id);

            return NoContent();
        }
    }
}
