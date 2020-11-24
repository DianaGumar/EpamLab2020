using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.Web.Models;

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
        [Authorize(Roles = "eventmanager")]
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
        [Authorize(Roles = "eventmanager")]
        public PartialViewResult SetPrice(int id = 0)
        {
            return PartialView("_SetPrice", _tmeventareaService.GetTMEventArea(id));
        }

        [HttpGet]
        [Authorize(Roles = "authorizeduser")]
        public ActionResult AreasMap(int idEvent)
        {
            if (idEvent <= 0)
            {
                return HttpNotFound();
            }

            List<TMEventAreaDto> objsareas = _tmeventareaService.GetAllTMEventArea()
                .Where(a => a.TMEventId == idEvent).ToList();

            var areas = new List<TMEventAreaViewModel>();

            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            foreach (var item in objsareas)
            {
                var localSeatsView = new List<TMEventSeatViewModel>();

                foreach (var itemchild in item.TMEventSeats)
                {
                    localSeatsView.Add(new TMEventSeatViewModel
                    {
                        Id = itemchild.Id,
                        State = itemchild.State,
                        Number = itemchild.Number,
                        Row = itemchild.Row,
                    });
                }

                areas.Add(new TMEventAreaViewModel
                {
                     Id = item.Id,
                     Price = '$' + item.Price.ToString("G", cultures),
                     CoordX = item.CoordX,
                     CoordY = item.CoordY,
                     CountSeatsX = item.TMEventSeats.Max(s => s.Number),
                     CountSeatsY = item.TMEventSeats.Max(s => s.Row),
                     Description = item.Description,
                     Seats = localSeatsView,
                });
            }

            return PartialView("_AreasMap", areas);
        }

        ////[HttpGet]
        ////[Authorize(Roles = "authorizeduser")]
        ////public ActionResult ChangeSeatState(List<TMEventAreaViewModel> model, int areaId, int id = 0)
        ////{
        ////    // something wrong
        ////    var seat = model.First(a => a.Id == areaId).Seats.First(s => s.Id == id);

        ////    seat.State = seat.State == SeatState.Free ? SeatState.Chousen : SeatState.Free;

        ////    return View("AreasMap", model);
        ////}
    }
}
