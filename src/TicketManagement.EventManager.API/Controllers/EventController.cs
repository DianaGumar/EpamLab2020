﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ITMEventService _tmeventService;

        public EventController(ITMEventService tmeventService)
        {
            _tmeventService = tmeventService;
        }

        // GET: api/<TMEventController>
        [HttpGet("existing-events")]
        public ActionResult<IEnumerable<TMEventDto>> GetAllExistingEvents() ////+
        {
            var user = HttpContext.User;
            _ = user;

            List<TMEventDto> models = _tmeventService.GetAllTMEvent()
                .OrderBy(u => u.StartEvent).ToList();

            return models;
        }

        // GET: api/<TMEventController>
        [HttpGet]
        public ActionResult<IEnumerable<TMEventDto>> Get() ////+
        {
            List<TMEventDto> models = _tmeventService.GetAllRelevantTMEvent()
                .OrderBy(u => u.StartEvent).ToList();

            return models;
        }

        // GET api/<TMEventController>/5
        [HttpGet("{id}")]
        public ActionResult<TMEventDto> Get(int id) ////+
        {
            var obj = _tmeventService.GetTMEvent(id);
            return obj == null ? NotFound() : new ObjectResult(obj);
        }

        // POST api/<TMEventController>/Create
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public TMEventDto Create([FromBody] TMEventDto obj) ////+
        {
            TMEventStatus result = ModelState.IsValid ?
                _tmeventService.CreateTMEvent(obj) : TMEventStatus.ModelInvalid;

            // если евент не имеет постера - ставить дефолтную картинку - логика этого на ui
            if (obj != null)
            {
                obj.Status = result;
            }

            return obj;
        }

        // PUT api/<TMEventController>/5
        [HttpPut("{id}")]
        ////[ValidateAntiForgeryToken]
        public TMEventDto Edit(int id, [FromBody] TMEventDto obj) ////+
        {
            TMEventStatus result = ModelState.IsValid ?
                _tmeventService.UpdateTMEvent(id, obj) : TMEventStatus.ModelInvalid;

            if (obj != null)
            {
                obj.Status = result;
            }

            return obj;
        }

        // DELETE api/<TMEventController>/5
        [HttpDelete("{id}")]
        public string Delete(int id) ////+
        {
            var response = _tmeventService.RemoveTMEvent(id).ToString();

            return response;
        }
    }
}
