using System.Collections.Generic;
using System.Linq;
////using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ITMEventService _tmeventService;
        private readonly ITMLayoutService _tmlayoutService;
        private readonly IVenueService _venueService;

        public EventsController(ITMEventService tmeventService,
            ITMLayoutService tmlayoutService, IVenueService venueService)
        {
            _tmeventService = tmeventService;
            _tmlayoutService = tmlayoutService;
            _venueService = venueService;
        }

        // GET: api/<TMEventController>
        //////[Authorize(Roles = "eventmanager")]
        [HttpGet("existing-events")]
        public ActionResult<IEnumerable<TMEventDto>> GetAllExistingEvents()
        {
            List<TMEventDto> models = _tmeventService.GetAllTMEvent()
                .OrderBy(u => u.StartEvent).Reverse().ToList();

            return models;
        }

        // GET: api/<TMEventController>
        [HttpGet]
        public ActionResult<IEnumerable<TMEventDto>> Get()
        {
            List<TMEventDto> models = _tmeventService.GetAllRelevantTMEvent()
                .OrderBy(u => u.StartEvent).Reverse().ToList();

            return models;
        }

        // GET api/<TMEventController>/5
        [HttpGet("{id}")]
        public ActionResult<TMEventDto> Get(int id)
        {
            var obj = _tmeventService.GetTMEvent(id);
            return obj == null ? NotFound() : new ObjectResult(obj);
        }

        // POST api/<TMEventController>/Create
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        //////[Authorize(Roles = "eventmanager")]
        public object Create([FromBody] TMEventDto obj)
        {
            // отображать картинку на интерфейсе, если событие не имеет постера
            string result = ModelState.IsValid ?
                _tmeventService.CreateTMEvent(obj).ToString() : "ModelInvalid";

            return new { result, obj };
        }

        // PUT api/<TMEventController>/5
        [HttpPut("{id}")]
        ////[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "eventmanager")]
        public string Edit(int id, [FromBody] TMEventDto obj)
        {
            if (ModelState.IsValid)
            {
                return _tmeventService.UpdateTMEvent(id, obj).ToString();
            }

            return "ModelInvalid";
        }

        // DELETE api/<TMEventController>/5
        [HttpDelete("{id}")]
        ////[Authorize(Roles = "eventmanager")]
        public string Delete(int id)
        {
            return _tmeventService.RemoveTMEvent(id).ToString();
        }
    }
}
