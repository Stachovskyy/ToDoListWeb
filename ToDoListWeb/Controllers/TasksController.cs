using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<List<WorkTaskModel>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAll();
            var tasksModel = _mapper.Map<List<WorkTaskModel>>(tasks);
            return tasksModel;
        }

        [HttpPost]
        public async Task<ActionResult<WorkTaskModel>> AddTask()
        {

        }


    }
}
