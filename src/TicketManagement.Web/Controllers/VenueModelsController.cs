using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.DAL;
using TicketManagement.Domain;

namespace TicketManagement.Web.Controllers
{
    public class VenueModelsController : Controller
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly IVenueBL _venuebl;

        public VenueModelsController()
        {
            // until dependensy ingection is include
            _venuebl = new VenueBL(
                new VenueService(new VenueRepository(_str)),
                new TMLayoutService(new TMLayoutRepository(_str)),
                new AreaService(new AreaRepository(_str)),
                new SeatService(new SeatRepository(_str)));
        }

        // GET: VenueModels
        [HttpGet]
        public ActionResult IndexVenueOnlyNames()
        {
            List<VenueModels> models =
               _venuebl.GetAllVenues().OrderBy(u => u.Lenght + u.Weidth).ToList();

            return View(models);
        }

        // GET: VenueModels
        [HttpGet]
        public ActionResult IndexLayoutOnlyNames(int venueId)
        {
            List<TMLayoutModels> models =
               _venuebl.GetAllLayouts().Where(l => l.VenueId == venueId).ToList();

            return PartialView("_IndexLayoutOnlyNames", models);
        }
    }
}
