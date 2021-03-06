﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.Controllers
{
    public class TMEventModelsController : Controller
    {
        private readonly ITMEventService _tmeventService;
        private readonly ITMLayoutService _tmlayoutService;
        private readonly IVenueService _venueService;

        public TMEventModelsController(ITMEventService tmeventService,
            ITMLayoutService tmlayoutService, IVenueService venueService)
        {
            _tmeventService = tmeventService;
            _tmlayoutService = tmlayoutService;
            _venueService = venueService;
        }

        // GET: TMEventModels
        [HttpGet]
        public ActionResult Index()
        {
            List<TMEventDto> models =
                _tmeventService.GetAllRelevantTMEvent().OrderBy(u => u.StartEvent).Reverse().ToList();

            return View(models);
        }

        // GET: TMEventModels
        [HttpGet]
        [Authorize(Roles = "eventmanager")]
        public ActionResult ListAllExistingItems()
        {
            List<TMEventDto> models =
                _tmeventService.GetAllTMEvent().OrderBy(u => u.StartEvent).Reverse().ToList();

            return View("Index", models);
        }

        // GET: TMEventModels/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            TMEventDto obj = _tmeventService.GetTMEvent(id);

            return View(obj);
        }

        private List<SelectListItem> GetVenueLayoutNames()
        {
            List<VenueDto> venues = _venueService.GetAllVenue();
            List<TMLayoutDto> layouts = _tmlayoutService.GetAllTMLayout();
            List<TMLayoutVenueViewModel> layoutsView = new List<TMLayoutVenueViewModel>();

            foreach (var item in venues)
            {
                layouts.Where(l => l.VenueId == item.Id).ToList().ForEach(l =>
                {
                    layoutsView.Add(new TMLayoutVenueViewModel { TMLayout = l, VenueName = item.Description });
                });
            }

            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            var items = new List<SelectListItem>();
            layoutsView.ForEach(l => items.Add(new SelectListItem
            {
                Text = l.VenueName + "--" + l.TMLayout.Description,
                Value = l.TMLayout.Id.ToString("G", cultures),
            }));

            return items;
        }

        // GET: TMEventModels/Create
        [HttpGet]
        [Authorize(Roles = "eventmanager")]
        public ActionResult Create()
        {
            return View(new TMEventViewModel
            {
                TMLayouts = GetVenueLayoutNames(),
                TMEvent = new TMEventDto(),
            });
        }

        // POST: TMEventModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "eventmanager")]
        public ActionResult Create([Bind] TMEventViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                var result = _tmeventService.CreateTMEvent(obj.TMEvent);

                switch (result)
                {
                    case TMEventStatus.Success:
                        return RedirectToAction("SetSeveralPrice", "Purchase", new { idEvent = obj.TMEvent.Id });
                    case TMEventStatus.DateInPast:
                        ModelState.AddModelError("", "date is in a past"); break;
                    case TMEventStatus.DateWrongOrder:
                        ModelState.AddModelError("", "end date before start date"); break;
                    case TMEventStatus.SameByDateObj:
                        ModelState.AddModelError("", "this venue is busy at this time"); break;
                    default:
                        ModelState.AddModelError("", "something wrong"); break;
                }
            }

            obj = obj ?? new TMEventViewModel { TMEvent = new TMEventDto() };
            obj.TMLayouts = GetVenueLayoutNames();

            return View(obj);
        }

        // GET: TMEventModels/Edit/5
        [HttpGet]
        [Authorize(Roles = "eventmanager")]
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
        [Authorize(Roles = "eventmanager")]
        public ActionResult Edit(int id, [Bind] TMEventDto obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                obj.Id = id;
                var result = _tmeventService.UpdateTMEvent(obj);

                switch (result)
                {
                    case TMEventStatus.Success:
                        return RedirectToAction("SetSeveralPrice", "Purchase", new { idEvent = obj.Id });
                    case TMEventStatus.DateInPast:
                        ModelState.AddModelError("", "date is in a past"); break;
                    case TMEventStatus.DateWrongOrder:
                        ModelState.AddModelError("", "end date before start date"); break;
                    case TMEventStatus.SameByDateObj:
                        ModelState.AddModelError("", "this venue is busy with another event at this time"); break;
                    case TMEventStatus.BusySeatsExists:
                        ModelState.AddModelError("", "you has bought ticket on this layout"); break;
                    default:
                        ModelState.AddModelError("", "something wrong"); break;
                }
            }

            return View(obj);
        }

        // POST: TMEventModels/Delete/5
        [HttpGet]
        [Authorize(Roles = "eventmanager")]
        public ActionResult Delete(int id = 0)
        {
            _tmeventService.RemoveTMEvent(id);

            return RedirectToAction("Index");
        }
    }
}
