using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Controllers
{
    public class TMEventAreaModelsController : Controller
    {
        private readonly ITMEventAreaService _tmeventareaService;

        public TMEventAreaModelsController(ITMEventAreaService tmeventareaService)
        {
            _tmeventareaService = tmeventareaService;
        }

        [HttpGet]
        public ActionResult Index(int idEvent = 0)
        {
            List<TMEventAreaDto> objs = _tmeventareaService.GetAllTMEventArea()
                .Where(a => a.TMEventId == idEvent).ToList();

            return View(objs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPrice(int id, [Bind] TMEventAreaDto obj)
        {
            if (obj != null)
            {
                obj.Id = id;
                _tmeventareaService.SetTMEventAreaPrice(obj.Id, obj.Price);
            }

            return RedirectToAction("Index", new { idEvent = obj?.TMEventId });
        }

        [HttpGet]
        public PartialViewResult SetPrice(int id = 0)
        {
            return PartialView("_SetPrice", _tmeventareaService.GetTMEventArea(id));
        }

        [HttpGet]
        public ActionResult AreasMap(int idEvent)
        {
            List<TMEventAreaDto> objs = _tmeventareaService.GetAllTMEventArea()
                .Where(a => a.TMEventId == idEvent).ToList();

            return View(objs);
        }

        [HttpGet]
        public PartialViewResult SeatsMap(int idArea)
        {
            List<TMEventSeatDto> objs = _tmeventareaService.GetTMEventSeatsByArea(idArea);

            return PartialView("_SeatsMap", objs);
        }

        [HttpGet]
        public ActionResult ChangeSeatState(int id = 0, SeatState state = 0, int areaId = 0)
        {
            state = (int)state < Enum.GetNames(typeof(SeatState)).Length - 1 ? state + 1 : 0;

            _tmeventareaService.SetTMEventSeatState(id, state);

            int idEvent = _tmeventareaService.GetTMEventArea(areaId).TMEventId;

            return RedirectToAction("AreasMap", new { idEvent });
        }
    }
}
