////using System.Collections.Generic;
////using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
////using TicketManagement.BusinessLogic.Standart.IServices;
////using TicketManagement.Domain.DTO;

namespace TicketManagement.EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TMEventController : ControllerBase
    {
        ////private readonly ITMEventService _tmeventService;
        ////private readonly ITMLayoutService _tmlayoutService;
        ////private readonly IVenueService _venueService;

        ////public TMEventController(ITMEventService tmeventService,
        ////    ITMLayoutService tmlayoutService, IVenueService venueService)
        ////{
        ////    _tmeventService = tmeventService;
        ////    _tmlayoutService = tmlayoutService;
        ////    _venueService = venueService;
        ////}

        // GET: api/<TMEventController>
        [Authorize]
        [HttpGet]
        public IActionResult Get() ////IEnumerable<string>
        {
            ////List<TMEventDto> models = _tmeventService.GetAllRelevantTMEvent()
            ////    .OrderBy(u => u.StartEvent).Reverse().ToList();

            _ = HttpContext.User;

            return Ok("Some data! =)");
        }

        ////// GET api/<TMEventController>/5
        ////[HttpGet("{id}")]
        ////public string Get(int id)
        ////{
        ////    return "value";
        ////}

        ////// POST api/<TMEventController>
        ////[HttpPost]
        ////public void Post([FromBody] string value)
        ////{
        ////}

        ////// PUT api/<TMEventController>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        ////// DELETE api/<TMEventController>/5
        ////[HttpDelete("{id}")]
        ////public void Delete(int id)
        ////{
        ////}
    }
}
