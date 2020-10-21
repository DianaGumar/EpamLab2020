using System.Web.Mvc;

namespace TicketManagement.Web.Controllers
{
    public class TMEventController : Controller
    {
        // GET: TMEvent
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}