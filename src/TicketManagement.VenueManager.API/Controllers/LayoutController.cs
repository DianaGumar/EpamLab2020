using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.VenueManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly ITMLayoutService _tmlayoutService;

        public LayoutController(ITMLayoutService tmlayoutService)
        {
            _tmlayoutService = tmlayoutService;
        }

        // GET: api/<LayoutController>
        [HttpGet]
        public ActionResult<IList<TMLayoutDto>> Get()
        {
            var models = _tmlayoutService.GetAllTMLayout().ToList();
            return models;
        }

        // GET api/<LayoutController>/5
        [HttpGet("{id}")]
        public ActionResult<TMLayoutDto> Get(int id)
        {
            var obj = _tmlayoutService.GetTMLayout(id);
            return obj == null ? NotFound() : new ObjectResult(obj);
        }

        ////// POST api/<LayoutController>
        ////[HttpPost]
        ////public void Post([FromBody] string value)
        ////{
        ////}

        ////// PUT api/<LayoutController>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        ////// DELETE api/<LayoutController>/5
        ////[HttpDelete("{id}")]
        ////public void Delete(int id)
        ////{
        ////}
    }
}
