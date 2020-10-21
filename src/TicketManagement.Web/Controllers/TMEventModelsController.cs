using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;

// until dependensy ingection is include
using TicketManagement.DataAccess.DAL;
using TicketManagement.Domain;

namespace TicketManagement.Web.Controllers
{
    public class TMEventModelsController : Controller
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly ITMEventBL _tmeventbl;

        public TMEventModelsController()
        {
            // until dependensy ingection is include
            _tmeventbl = new TMEventBL(
                new TMEventService(new TMEventRepository(_str)),
                new AreaService(new AreaRepository(_str)),
                new SeatService(new SeatRepository(_str)),
                new TMEventAreaService(new TMEventAreaRepository(_str)),
                new TMEventSeatService(new TMEventSeatRepository(_str)));
        }

        // GET: TMEventModels
        [HttpGet]
        public ActionResult ReadAll()
        {
            var tmeventPre = new TMEventModels
            {
                Name = "a2333",
                Description = "d2333",
                StartEvent = DateTime.Now.Date.AddDays(10),
                EndEvent = DateTime.Now.Date.AddDays(11),
                TMLayoutId = 1,
            };

            var tmeventPre2 = new TMEventModels
            {
                Name = "a2222",
                Description = "d2222",
                StartEvent = DateTime.Now.Date.AddDays(5),
                EndEvent = DateTime.Now.Date.AddDays(6),
                TMLayoutId = 1,
            };

            _tmeventbl.CreateTMEvent(tmeventPre);
            _tmeventbl.CreateTMEvent(tmeventPre2);

            List<TMEventModels> models = _tmeventbl.GetAllTMEvent();

            return View(models);
        }
    }
}