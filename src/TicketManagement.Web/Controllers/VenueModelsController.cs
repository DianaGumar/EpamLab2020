using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.DAL;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Controllers
{
    public class VenueModelsController : Controller
    {
        private readonly string _str = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly IVenueService _venueService;

        public VenueModelsController()
        {
            // until dependensy ingection is include
            _venueService = new VenueService(
                new VenueRepository(_str),
                new TMLayoutService(new TMLayoutRepository(_str)),
                new AreaService(new AreaRepository(_str)),
                new SeatService(new SeatRepository(_str)));
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<VenueDto> models =
               _venueService.GetAllVenue().OrderBy(u => u.Lenght + u.Weidth).Reverse().ToList();

            return View(models);
        }

        // GET: VenueModels
        [HttpGet]
        public PartialViewResult IndexVenueOnlyNames()
        {
            List<VenueDto> models =
               _venueService.GetAllVenue().OrderBy(u => u.Lenght + u.Weidth).ToList();

            return PartialView("_IndexVenueOnlyNames", models);
        }

        // GET: VenueModels
        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult IndexLayoutOnlyNames(int venueId)
        {
            List<TMLayoutDto> models = _venueService.GetAllLayoutByVenue(venueId);

            return PartialView("_IndexLayoutOnlyNames", models);
        }
    }
}