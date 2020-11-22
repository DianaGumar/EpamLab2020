using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaceService _purchaceService;

        public PurchaseController(IPurchaceService purchaceService)
        {
            _purchaceService = purchaceService;
        }

        [Authorize(Roles = "authorizeduser")]
        [HttpGet]
        public ActionResult Index()
        {
            List<PurchaseHistoryDto> modelsDto =
                _purchaceService.GetPurchaseHistory(User.Identity.GetUserId())
                    .OrderBy(u => u.BookingDate).Reverse().ToList();

            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            var models = new List<PurchaseHistoryViewModel>();

            foreach (var item in modelsDto)
            {
                models.Add(new PurchaseHistoryViewModel
                {
                    BookingDate = item.BookingDate.ToString("G", cultures),
                    Cost = item.AreaPrice.ToString("G", cultures),
                    SeatRow = item.SeatObj.Row.ToString("G", cultures),
                    SeatNumber = item.SeatObj.Number.ToString("G", cultures),
                    EventName = item.TMEventObj.Name,
                    EventLast = item.TMEventObj.StartEvent < DateTime.Now,
                });
            }

            return View(models);
        }
    }
}