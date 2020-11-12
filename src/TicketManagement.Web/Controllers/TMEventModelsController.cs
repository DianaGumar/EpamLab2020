using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Controllers
{
    public class TMEventModelsController : Controller
    {
        private readonly ITMEventService _tmeventService;

        public TMEventModelsController(ITMEventService tmeventService)
        {
            _tmeventService = tmeventService;
        }

        // GET: TMEventModels
        [HttpGet]
        public ActionResult Index()
        {
            List<TMEventDto> models =
                _tmeventService.GetAllTMEvent().OrderBy(u => u.StartEvent).Reverse().ToList();

            return View(models);
        }

        // GET: TMEventModels/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            TMEventDto obj = _tmeventService.GetTMEvent(id);

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
        public ActionResult Create([Bind] TMEventDto obj)
        {
            if (ModelState.IsValid)
            {
                _tmeventService.CreateTMEvent(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: TMEventModels/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TMEventDto obj = _tmeventService.GetTMEvent(id);

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
        public ActionResult Edit(int id, [Bind] TMEventDto obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                obj.Id = id;
                _tmeventService.UpdateTMEvent(obj);

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // POST: TMEventModels/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            _tmeventService.RemoveTMEvent(id);

            return RedirectToAction("Index");
        }

        ////protected override void Dispose(bool disposing)
        ////{
        ////    _context.Dispose();
        ////    base.Dispose(disposing);
        ////}
    }
}
