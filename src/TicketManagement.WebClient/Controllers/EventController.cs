using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Domain.DTO;

namespace TicketManagement.WebClient.Controllers
{
    public class EventController : Controller
    {
        private HttpClient _httpClient;

        public EventController()
        {
            _httpClient = new HttpClient();

#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5003/");
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: EventController
        public async Task<ActionResult> Index()
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Event");
            return View(tmevents);
        }

        // GET: EventController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
            return View(tmevent);
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Event", collection);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return View();
            }
        }

        ////// GET: EventController/Edit/5
        ////public ActionResult Edit(int id)
        ////{
        ////    return View();
        ////}

        ////// POST: EventController/Edit/5
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Edit(int id, IFormCollection collection)
        ////{
        ////    try
        ////    {
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}

        ////// GET: EventController/Delete/5
        ////public ActionResult Delete(int id)
        ////{
        ////    return View();
        ////}

        ////// POST: EventController/Delete/5
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Delete(int id, IFormCollection collection)
        ////{
        ////    try
        ////    {
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}

        protected override void Dispose(bool disposing)
        {
            if (disposing && _httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }

            base.Dispose(disposing);
        }
    }
}
