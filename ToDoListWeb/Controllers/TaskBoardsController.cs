using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Data.Repositories;
using ToDoListWeb.Entities;
using ToDoListWeb.Exceptions;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    [Route("api/[controller]")]   //
    [ApiController]
    public class TaskBoardsController : ControllerBase
    {
        private readonly ITaskBoardRepository _taskBoardRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly MainContext _context;

        public TaskBoardsController(ITaskBoardRepository taskBoardRepository, IMapper mapper, UserManager<User> userManager, MainContext context)
        {
            _taskBoardRepository = taskBoardRepository;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<TaskBoardModel>>> Get()
        {
            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoards = await _taskBoardRepository.GetAllTaskBoardsAsync(user);

            if (taskBoards == null)
            {
                throw new NotFoundException("Could not find any taskboards");
            }
            var taskBoardToTeturn = _mapper.Map<List<TaskBoardModel>>(taskBoards);
            return taskBoardToTeturn;
        }

        [Authorize]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TaskBoardModel>> GetSingle(int Id)
        {
            var loggedUser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoards = await _taskBoardRepository.GetSingleTaskBoardAsync(user, Id);

            if (taskBoards == null)
            {
                throw new NotFoundException("Could not find any taskboards");
            }
            var taskBoardToTeturn = _mapper.Map<TaskBoardModel>(taskBoards);
            return taskBoardToTeturn;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskBoardModel>> Post([FromBody] TaskBoardModel model)
        {
            var loggedUser = User.Identity.Name;                     

            var user = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = _mapper.Map<TaskBoard>(model);

            var addedTaskBoard = await _taskBoardRepository.AddSingleTaskBoard(user, taskBoard);

            return _mapper.Map<TaskBoardModel>(addedTaskBoard);

        }

        [Authorize]
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<TaskBoardModel>> AddUserToTaskBoard(string user, TaskBoardModel model, [System.Web.Http.FromUri] int Id)
        {
            var loggedUser = User.Identity.Name;

            var mainUser = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(mainUser, Id);
            if (taskBoard == null)
            {
                throw new NotFoundException("There is no TaskBoard with this Id");
            }

            taskBoard=_mapper.Map(model, taskBoard);

            var wholeUser = await _userManager.FindByNameAsync(user);

            if(wholeUser==null)
            {
                throw new NotFoundException("There is no user with this username");
            }

           var updatedtask = await _taskBoardRepository.AddTaskBoardToAnotherUser(wholeUser, taskBoard);

            return _mapper.Map<TaskBoardModel>(updatedtask);

        }

        [Authorize]
        [HttpPatch("{Id:int}")]
        public async Task<ActionResult<TaskBoardModel>> ChangeName(TaskBoardModelWithoutList model, [System.Web.Http.FromUri] int Id)
        {
            var loggedUser = User.Identity.Name;

            var mainUser = await _userManager.FindByNameAsync(loggedUser);

            var taskBoard = await _taskBoardRepository.GetSingleTaskBoardAsync(mainUser, Id);
            if (taskBoard == null)
            {
                throw new NotFoundException("There is no TaskBoard with this Id");
            }

            taskBoard = _mapper.Map(model, taskBoard);

            await _taskBoardRepository.SaveChangesAsync();


            return _mapper.Map<TaskBoardModel>(taskBoard);

        }

        [Authorize]
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> SoftDelete(int Id)
        {
            var loggeduser = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(loggeduser);

            await _taskBoardRepository.SoftDelete(user, Id);

            return NoContent(); 
        }
    }
}
