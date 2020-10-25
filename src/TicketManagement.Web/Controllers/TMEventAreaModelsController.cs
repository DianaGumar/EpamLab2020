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
            _tmeventareabl = new TMEventAreasBL(new TMEventAreaService(new TMEventAreaRepository(_str)));
        }

        // GET: TMEventAreaModels/SetPrice/5
        [HttpGet]
        public ActionResult Index(int idEvent)
        {
            List<TMEventAreaModels> objs = _tmeventareabl.GetAllTMEventAreas()
                .Where(a => a.TMEventId == idEvent).ToList();

            // return View()
            return PartialView("_Index", objs);
        }

        [HttpGet]
        public ActionResult SetPrice(int id)
        {
            return PartialView("_SetPrice", _tmeventareabl.GetTMEventArea(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPrice(int id, decimal prise)
        {
            _tmeventareabl.SetTMEventAreaPrice(id, prise);

            return PartialView("_SetPrice");
        }
    }
}
