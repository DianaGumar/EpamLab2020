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
    public class TMEventModelsController : Controller
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly ITMEventBL _tmeventbl;

        public TMEventModelsController()
        {
            // until dependensy ingection is include
            _tmeventbl = new TMEventBL(
                new TMEventService(new TMEventRepository(_str)),
                new AreaService(new AreaRepository(_str)),
                new SeatService(new SeatRepository(_str)),
                new TMEventAreaService(new TMEventAreaRepository(_str)),
                new TMEventSeatService(new TMEventSeatRepository(_str)));
        }

        // GET: TMEventModels
        [HttpGet]
        public ActionResult Index()
        {
            List<TMEventModels> models =
                _tmeventbl.GetAllTMEvent().OrderBy(u => u.StartEvent).Reverse().ToList();

            return View(models);
        }

        // GET: TMEventModels/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            TMEventModels obj = _tmeventbl.GetTMEvent(id);

            return View(obj);
        }

        // GET: TMEventModels/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TMEventModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] TMEventModels obj)
        {
            if (ModelState.IsValid)
            {
                _tmeventbl.CreateTMEvent(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: TMEventModels/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TMEventModels obj = _tmeventbl.GetTMEvent(id);

            if (obj == null)
            {
                RedirectToAction("Index");
            }

            // return View()
            return View(obj);
        }

        // POST: TMEventModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] TMEventModels obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                obj.TMEventId = id;
                _tmeventbl.UpdateTMEvent(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // POST: TMEventModels/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            _tmeventbl.DeleteTMEvent(id);

            return RedirectToAction("Index");
        }
    }
}
