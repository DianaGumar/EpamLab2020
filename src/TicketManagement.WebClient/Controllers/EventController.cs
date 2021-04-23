using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.Domain.DTO;
using TicketManagement.WebClient.Models;

namespace TicketManagement.WebClient.Controllers
{
    public class EventController : Controller
    {
        private HttpClient _httpClient;

        public EventController()
        {
            _httpClient = new HttpClient();

#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5101/");
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

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            _ = await _httpClient.DeleteAsync($"api/Event/{id}");
            return RedirectToAction(nameof(Index));
        }

        // доделать нормальное представление
        // GET: EventLayoutController/Create
        public async Task<ActionResult> Create()
        {
            // использует полную модель представления EventLayoutViewModel
            // получаем все возможные лайауты для преедачи в представление
            var layouts = await _httpClient.GetFromJsonAsync<List<TMLayoutDto>>("api/Layout");

            var layoutStr = layouts.Select(l => "id=" + l.Id + " name=" + l.Description).ToArray();
            SelectList selectList = new SelectList(layoutStr);

            return View(new EventLayoutViewModel
            {
                TMEvent = new TMEventDto(),
                TMLayouts = selectList,
            });
        }

        // POST: EventLayoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                _ = await _httpClient.PostAsJsonAsync("api/Event", collection);
                return RedirectToAction(nameof(Index));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return View();
            }
        }

        // GET: EventLayoutController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // получаем все возможные лайауты для преедачи в представление
            var layouts = await _httpClient.GetFromJsonAsync<List<TMLayoutDto>>("api/Layout");
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");

            return View(new { tmevent, layouts });
        }

        // POST: EventLayoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                _ = await _httpClient.PutAsJsonAsync($"api/Event/{id}", collection);
                return RedirectToAction(nameof(Index));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return View();
            }
        }

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
