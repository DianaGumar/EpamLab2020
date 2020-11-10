using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess;
using TicketManagement.DataAccess.DAL;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Controllers
{
    public class VenueModelsController : Controller
    {
        // private readonly string _str = ConfigurationManager
        // .ConnectionStrings["DefaultConnection"].ConnectionString
        private readonly TMContext _context;

        private readonly IVenueService _venueService;

        public VenueModelsController()
        {
            _context = new TMContext();

            // until dependensy ingection is include
            _venueService = new VenueService(
                new VenueRepositoryEF(_context),
                new TMLayoutService(new TMLayoutRepositoryEF(_context)),
                new AreaService(new AreaRepositoryEF(_context)),
                new SeatService(new SeatRepositoryEF(_context)));
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}