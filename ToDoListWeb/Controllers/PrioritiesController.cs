using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    [Route("api/TaskBoards/{taskBoardId}/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository _priorityRepository;
        private readonly IMapper _mapper;
        /*       Plan  
         * 1.Wypierdolic Size        +++?
         * 2.Zrobic StatusyHardCode +++?
         * 3.walidacje +-?
         * 
         * 3.1 Obsługa błędów +++
         * 4.Sprzątniecie kodu 
         * 4.1 Namespace !  ctrl+k ctrle+E
         * 4.2 ctrl+k+d
         * 5. Dodanie użytkowników
         * 6.Dodac swaggera
         * 7.Paging
         * */

        public PrioritiesController(IPriorityRepository priorityRepository, IMapper mapper)
        {
            _priorityRepository = priorityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PriorityModel>>> GetAll()
        {
            var priorities = await _priorityRepository.GetPrioritiesAsync();

            var prioritiesmodel = _mapper.Map<List<PriorityModel>>(priorities);

            return prioritiesmodel;
        }

        [HttpPost]
        public async Task<ActionResult<PriorityModel>> CreatePriority(PriorityModel priority)
        {
            var existingpriority = _priorityRepository.GetPriorityByName(priority.Name);

            if (existingpriority != null)
                return BadRequest("Priority with that name already exists");

            var mappedModel = _mapper.Map<Priority>(priority);

            var createdPriority = await _priorityRepository.AddPriority(mappedModel);

            var returnedModel = _mapper.Map<PriorityModel>(createdPriority);

                return StatusCode((int)HttpStatusCode.Created, returnedModel);
        }

        [HttpPut]
        public async Task<ActionResult<PriorityModel>> Update(int priorityId, PriorityModel model)
        {

            var oldPriority = await _priorityRepository.GetPriority(priorityId);

            if (oldPriority == null)
                return NotFound("Could not find Priority");

            _mapper.Map(model, oldPriority);  //updejtuje priority
                return _mapper.Map<PriorityModel>(oldPriority);   //spowrotem na model
        }

        [HttpDelete("priorityId:int")]       //jak to zrobic ?
        public async Task<IActionResult> DeletePriority(int priorityId)
        {
            await _priorityRepository.SoftDelete(priorityId);

            return NoContent(); // pusty ok = nocontent
        }
    }
}
