using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaceService _purchaceService;
        private readonly ITMEventAreaService _tmeventAreaService;

        public PurchaseController(IPurchaceService purchaceService,
            ITMEventAreaService tmeventAreaService)
        {
            _purchaceService = purchaceService;
            _tmeventAreaService = tmeventAreaService;
        }

        [Authorize(Roles = "authorizeduser")]
        [HttpGet]
        public ActionResult Index()
        {
            List<PurchaseHistoryDto> modelsDto =
                _purchaceService.GetPurchaseHistory(User.Identity.GetUserId())
                    .OrderBy(u => u.BookingDate).Reverse().ToList();

            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            var models = new List<PurchaseHistoryViewModel>();

            foreach (var item in modelsDto)
            {
                models.Add(new PurchaseHistoryViewModel
                {
                    Id = item.SeatObj.Id.ToString("G", cultures),
                    BookingDate = item.BookingDate.ToString("G", cultures),
                    Cost = item.AreaPrice.ToString("G", cultures),
                    SeatRow = item.SeatObj.Row.ToString("G", cultures),
                    SeatNumber = item.SeatObj.Number.ToString("G", cultures),
                    EventName = item.TMEventObj.Name,
                    EventLast = item.TMEventObj.StartEvent < DateTime.Now,
                });
            }

            return View(models);
        }

        [Authorize(Roles = "authorizeduser")]
        [HttpGet]
        public ActionResult ReturnTicket(int seatsId = 0)
        {
            PurchaseStatus result =
                    _purchaceService.ReturnTicket(User.Identity.GetUserId(), seatsId);

            switch (result)
            {
                case PurchaseStatus.ReturnTicketSucsess:
                    return Redirect("Index");
                case PurchaseStatus.ReturnTicketFailWithPastEvent:
                    ModelState.AddModelError("", "Event is olready in past. You cant't return tiket"); break;
                default:
                    ModelState.AddModelError("", "something wrong"); break;
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "authorizeduser")]
        [HttpGet]
        public ActionResult BuyTicket(int idEvent)
        {
            return View(new TMEventSeatIdViewModel { TMEventId = idEvent });
        }

        [Authorize(Roles = "authorizeduser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyTicket(TMEventSeatIdViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }

            if (model.SeatsId == null || model.SeatsId.Length == 0)
            {
                ModelState.AddModelError("", "Chouse seats");
                return View(model);
            }

            int[] seatsId = SeatsChousenToValid(model.SeatsId);

            // real loggic
            if (seatsId.Length > 0)
            {
                PurchaseStatus result =
                    _purchaceService.BuyTicket(User.Identity.GetUserId(), seatsId);

                switch (result)
                {
                    case PurchaseStatus.PurchaseSucsess:
                        return Redirect("Index");
                    case PurchaseStatus.NotEnothMoney:
                        ModelState.AddModelError("", "You has not enoth money. Top up balance"); break;
                    case PurchaseStatus.Wait:
                        ModelState.AddModelError("", "Some one else buy seats now"); break;
                    case PurchaseStatus.NotRelevantSeats:
                        ModelState.AddModelError("", "Seats olready chousen or not exist"); break;
                    default:
                        ModelState.AddModelError("", "something wrong"); break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Validate error");
            }

            return View(model);
        }

        private static int[] SeatsChousenToValid(string model)
        {
            // making model in valid form
            string[] seatsIdstr = model.Split(',');

            var seatsId = new List<int>();

            int j = 0;
            for (int i = 0; i < seatsIdstr.Length; i++)
            {
                if (int.TryParse(seatsIdstr[i], out j))
                {
                    seatsId.Add(j);
                }
                else
                {
                    break;
                }
            }

            return seatsId.ToArray();
        }

        private List<TMEventAreaViewModel> GetAreas(int idEvent)
        {
            if (idEvent <= 0)
            {
                return new List<TMEventAreaViewModel>();
            }

            List<TMEventAreaDto> objsareas = _tmeventAreaService.GetAllTMEventArea()
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

            return areas;
        }

        // was supressed, cose nead the Same post and get methods for partial view
        [AcceptVerbsAttribute(HttpVerbs.Get| HttpVerbs.Post)]
        [Authorize(Roles = "authorizeduser")]
#pragma warning disable CA3147 // Mark Verb Handlers With Validate Antiforgery Token
        public ActionResult AreasMap(int idEvent)
#pragma warning restore CA3147 // Mark Verb Handlers With Validate Antiforgery Token
        {
            return PartialView("_AreasMap", GetAreas(idEvent));
        }

        [HttpGet]
        public ActionResult SetSeveralPrice(int idEvent = 0)
        {
            List<TMEventAreaDto> objs = _tmeventAreaService.GetAllTMEventArea()
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
                _tmeventAreaService.SetTMEventAreaPrice(obj.Id, obj.Price);
            }

            return RedirectToAction("SetSeveralPrice", new { idEvent = obj?.TMEventId });
        }

        [HttpGet]
        [Authorize(Roles = "eventmanager")]
        public PartialViewResult SetPrice(int id = 0)
        {
            return PartialView("_SetPrice", _tmeventAreaService.GetTMEventArea(id));
        }
    }
}