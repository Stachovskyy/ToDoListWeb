using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Exceptions;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    [Route("api/[controller]")]            
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository _priorityRepository;
        private readonly IMapper _mapper;
        

        public PrioritiesController(IPriorityRepository priorityRepository, IMapper mapper)
        {
            _priorityRepository = priorityRepository;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<ActionResult<List<PriorityModel>>> GetAll()
        {
            var priorities = await _priorityRepository.GetPrioritiesAsync();

            if (priorities == null)
                throw new NotFoundException("There is no priorities");

            var prioritiesmodel = _mapper.Map<List<PriorityModel>>(priorities);

            return prioritiesmodel;
        }

        [HttpPost]                                    
        public async Task<ActionResult<PriorityModel>> CreatePriority(
            PriorityModel priority)
        {
            var existingpriority =await _priorityRepository.GetPriorityByName(priority.Name);

            if (existingpriority != null)
                return BadRequest("Priority with that name already exists");

            var mappedModel = _mapper.Map<Priority>(priority);

            var createdPriority = await _priorityRepository.AddPriority(mappedModel);

            var returnedModel = _mapper.Map<PriorityModel>(createdPriority);

            return StatusCode((int)HttpStatusCode.Created, returnedModel);
        }

        [HttpPut]
        public async Task<ActionResult<PriorityModel>> Update(
            int priorityId,
            PriorityModel model)
        {

            var oldPriority = await _priorityRepository.GetPriority(priorityId);

            if (oldPriority == null)
                throw new NotFoundException("There is no priority to update");

            _mapper.Map(model, oldPriority);
            return _mapper.Map<PriorityModel>(oldPriority);
        }

        [HttpDelete("priorityId:int")]
        public async Task<IActionResult> DeletePriority(int priorityId)
        {
            if (_priorityRepository.GetPriority(priorityId) == null)
                throw new NotFoundException("There is no prority to delete");

            await _priorityRepository.SoftDelete(priorityId);

            return NoContent();
        }
    }
}
