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
        private readonly IVenueService _venueService;

        public TMEventModelsController(ITMEventService tmeventService, IVenueService venueService)
        {
            _tmeventService = tmeventService;
            _venueService = venueService;
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

        private List<SelectListItem> GetVenueLayoutNames()
        {
            List<VenueDto> venues = _venueService.GetAllVenue();
            List<TMLayoutVievModel> layouts = new List<TMLayoutVievModel>();

            foreach (var item in venues)
            {
                _venueService.GetAllLayoutByVenue(item.Id).ForEach(l =>
                {
                    layouts.Add(new TMLayoutVievModel { TMLayout = l, VenueName = item.Description });
                });
            }

            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            var items = new List<SelectListItem>();
            layouts.ForEach(l => items.Add(new SelectListItem
            {
                Text = l.VenueName + "--" + l.TMLayout.Description,
                Value = l.TMLayout.Id.ToString("G", cultures),
            }));

            return items;
        }

        // GET: TMEventModels/Create
        [HttpGet]
        ////[Authorize(Roles = "event_manager")]
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
        ////[Authorize(Roles = "event_manager")]
        public ActionResult Create([Bind] TMEventViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _tmeventService.CreateTMEvent(obj?.TMEvent);
                return RedirectToAction("Index");
            }

            obj = obj ?? new TMEventViewModel { TMEvent = new TMEventDto() };

            obj.TMLayouts = GetVenueLayoutNames();

            return View(obj);
        }

        // GET: TMEventModels/Edit/5
        [HttpGet]
        ////[Authorize(Roles = "event_manager")]
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
        ////[Authorize(Roles = "event_manager")]
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
        [Authorize(Roles = "event_manager")]
        public ActionResult Delete(int id = 0)
        {
            _tmeventService.RemoveTMEvent(id);

            return RedirectToAction("Index");
        }
    }
}
