using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]                 //CZY TO NIE POWINNO BYC api/TaskBoard ?? 
        public async Task<ActionResult<List<WorkTaskResponse>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var tasksModel = _mapper.Map<List<WorkTaskResponse>>(tasks);
            return tasksModel;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<WorkTaskResponse>> GetSingleTask(int Id)
        {
            var task = await _taskRepository.GetSingleAsync(Id);
            if (task == null) return NotFound("Could not found Task");
            return _mapper.Map<WorkTaskResponse>(task);
        }

        [HttpPost]
        public async Task<ActionResult<WorkTaskResponse>> AddTask(WorkTaskCreateModel model)
        {
            var task = _mapper.Map<WorkTask>(model);
            var createdTask = await _taskRepository.AddAsync(task);
            var taskmodel = _mapper.Map<WorkTaskResponse>(createdTask);
            return StatusCode((int)HttpStatusCode.Created, taskmodel);            //Dla 
        }

        [HttpPut("{Id:int}")]           // Co to powinno byc mozliwe zmiana statusu ?? nazwy czy jak ?

        public async Task<ActionResult<WorkTaskResponse>> Put(int Id,WorkTaskCreateModel model)
        {
            var oldTask = await _taskRepository.GetSingleAsync(Id);
            var updatedTask = _mapper.Map(model, oldTask);
            var returnedTask = _mapper.Map<WorkTaskResponse>(updatedTask);               //zwracam bez id statusu i sizu

            return returnedTask;
        }


        [HttpDelete("{Id:int}")]

        public async Task<IActionResult> Delete(int Id)
        {
            var taskToDelete = await _taskRepository.GetSingleAsync(Id);
            _taskRepository.Delete(taskToDelete);
            return Ok();
        }
    }
}
