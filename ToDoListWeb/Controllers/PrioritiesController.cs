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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<List<PriorityApiResponse>>> GetAllPriorities()
        {
            var priorities = await _priorityRepository.GetPrioritiesAsync();

            if (priorities == null)
            {
                throw new NotFoundException("There is no priorities");
            }
            var prioritiesModel = _mapper.Map<List<PriorityApiResponse>>(priorities);

            return prioritiesModel;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PriorityApiResponse>> GetSinglePriority(int id)
        {
            var priorities = await _priorityRepository.GetPriority(id);

            if (priorities == null)
            {
                throw new NotFoundException("There is no priority with this Id");
            }

            var prioritiesModel = _mapper.Map<PriorityApiResponse>(priorities);

            return prioritiesModel;
        }

        [HttpPost]
        public async Task<ActionResult<PriorityModel>> CreatePriority(PriorityModel priority)
        {
            var existingPriority = await _priorityRepository.GetPriorityByName(priority.Name);

            if (existingPriority != null)
            {
                return BadRequest("Priority with that name already exists");
            }

            Priority mappedModel = _mapper.Map<Priority>(priority);

            var createdPriority = await _priorityRepository.AddPriority(mappedModel);

            var returnedModel = _mapper.Map<PriorityApiResponse>(createdPriority);

            return StatusCode((int)HttpStatusCode.Created, returnedModel);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PriorityApiResponse>> UpdatePriority(
            int id,
            PriorityModel model)
        {
            var oldPriority = await _priorityRepository.GetPriority(id);

            if (oldPriority == null)
            {
                throw new NotFoundException("There is no priority to update");
            }

            _mapper.Map(model, oldPriority);

            await _priorityRepository.SaveChangesAsync();

            return _mapper.Map<PriorityApiResponse>(oldPriority);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            if (await _priorityRepository.GetPriority(id) == null)
            {
                throw new NotFoundException("There is no prority to delete");
            }

            await _priorityRepository.SoftDelete(id);

            return NoContent();
        }
    }
}
