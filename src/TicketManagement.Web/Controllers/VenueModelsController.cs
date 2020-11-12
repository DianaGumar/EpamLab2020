using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Controllers
{
    public class VenueModelsController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueModelsController(IVenueService venueService)
        {
            _venueService = venueService;
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