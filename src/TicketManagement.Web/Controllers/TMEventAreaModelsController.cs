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
    }
}
