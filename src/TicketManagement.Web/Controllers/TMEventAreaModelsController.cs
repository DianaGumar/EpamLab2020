using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.DAL;
using TicketManagement.Domain;

namespace TicketManagement.Web.Controllers
{
    public class TMEventAreaModelsController : Controller
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly ITMEventAreasBL _tmeventareabl;

        public TMEventAreaModelsController()
        {
            // until dependensy ingection is include
            _tmeventareabl = new TMEventAreasBL(
                new TMEventAreaService(new TMEventAreaRepository(_str)),
                new TMEventSeatService(new TMEventSeatRepository(_str)));
        }

        [HttpGet]
        public ActionResult Index(int idEvent = 0)
        {
            List<TMEventAreaModels> objs = _tmeventareabl.GetAllTMEventAreas()
                .Where(a => a.TMEventId == idEvent).ToList();

            return View(objs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPrice(int id, [Bind] TMEventAreaModels obj)
        {
            if (obj != null)
            {
                obj.TMEventAreaId = id;
                _tmeventareabl.SetTMEventAreaPrice(obj.TMEventAreaId, obj.Price);
            }

            return RedirectToAction("Index", new { idEvent = obj?.TMEventId });
        }

        [HttpGet]
        public PartialViewResult SetPrice(int id = 0)
        {
            return PartialView("_SetPrice", _tmeventareabl.GetTMEventArea(id));
        }

        [HttpGet]
        public ActionResult AreasMap(int idEvent)
        {
            List<TMEventAreaModels> objs = _tmeventareabl.GetAllTMEventAreas()
                .Where(a => a.TMEventId == idEvent).ToList();

            return View(objs);
        }

        [HttpGet]
        public PartialViewResult SeatsMap(int idArea)
        {
            List<TMEventSeatModels> objs = _tmeventareabl.GetTMEventSeats(idArea);

            return PartialView("_SeatsMap", objs);
        }

        [HttpGet]
        public ActionResult ChangeSeatState(int id = 0, SeatState state = 0, int areaId = 0)
        {
            state = (int)state < Enum.GetNames(typeof(SeatState)).Length - 1 ? state + 1 : 0;

            _tmeventareabl.SetTMEventSeatState(id, state);

            int idEvent = _tmeventareabl.GetTMEventArea(areaId).TMEventId;

            return RedirectToAction("AreasMap", new { idEvent });
        }
    }
}
