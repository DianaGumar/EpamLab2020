using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.Controllers
{
    public class VenueModelsController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly ISeatService _seatService;
        private readonly ITMLayoutService _tmlayoutService;
        private readonly IAreaService _areaService;

        public VenueModelsController(IVenueService venueService, ISeatService seatService,
            ITMLayoutService tmlayoutService, IAreaService areaService)
        {
            _venueService = venueService;
            _seatService = seatService;
            _tmlayoutService = tmlayoutService;
            _areaService = areaService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<VenueDto> models =
               _venueService.GetAllVenue().OrderBy(u => u.Lenght + u.Weidth).Reverse().ToList();

            return View(models);
        }

        [HttpGet]
        [Authorize(Roles = "authorizeduser")]
        public PartialViewResult IndexVenueOnlyNames()
        {
            List<VenueDto> models =
               _venueService.GetAllVenue().OrderBy(u => u.Lenght + u.Weidth).ToList();

            return PartialView("_IndexVenueOnlyNames", models);
        }

        [HttpGet]
        [ChildActionOnly]
        [Authorize(Roles = "authorizeduser")]
        public PartialViewResult IndexLayoutOnlyNames(int venueId)
        {
            List<TMLayoutDto> models = _tmlayoutService.GetAllTMLayout()
                .Where(l => l.VenueId == venueId).ToList();

            return PartialView("_IndexLayoutOnlyNames", models);
        }

        [HttpGet]
        public ActionResult LayoutMap(int idlayout)
        {
            if (idlayout <= 0)
            {
                return HttpNotFound();
            }

            List<SeatDto> objsseats = _seatService.GetAllSeat();
            List<AreaDto> objsareas = _areaService.GetAllArea().Where(a => a.TMLayoutId == idlayout).ToList();

            List<AreaViewModel> areaViewModel = new List<AreaViewModel>();

            List<SeatViewModel> localSeatsView = new List<SeatViewModel>();
            List<SeatDto> localSeats;
            foreach (var item in objsareas)
            {
                localSeats = objsseats.Where(s => s.AreaId == item.Id).ToList();

                foreach (var itemchild in localSeats)
                {
                    localSeatsView.Add(new SeatViewModel
                    {
                         Number = itemchild.Number,
                         Row = itemchild.Row,
                    });
                }

                areaViewModel.Add(new AreaViewModel
                {
                    CoordX = item.CoordX,
                    CoordY = item.CoordY,
                    CountSeatsX = localSeats.Max(s => s.Number),
                    CountSeatsY = localSeats.Max(s => s.Row),
                    Description = item.Description,
                    Seats = localSeatsView,
                });
            }

            TMLayoutViewModel layoutViewModel = new TMLayoutViewModel
            {
                Layout = _tmlayoutService.GetTMLayout(idlayout),
                Areas = areaViewModel,
            };

            return PartialView("_LayoutMap", layoutViewModel);
        }
    }
}