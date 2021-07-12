using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TicketManagement.BusinessLogic;
using TicketManagement.Domain.DTO;
using TicketManagement.WebClient.Models;

namespace TicketManagement.WebClient.Controllers
{
    public class EventController : Controller
    {
        private HttpClient _httpClient;

        public EventController(HttpClient httpClient)
        {
            _httpClient = httpClient;

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

        // GET: EventController
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            // достаём и отправляем с запросом токен
            var token = HttpContext.Request.Cookies["secret_jwt_key"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Event/all");
            return View(tmevents);
        }

        // GET: EventController/Details/5
        [Authorize(Roles = "eventmanager")]
        public async Task<ActionResult> Details(int id)
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
            return View(tmevent);
        }

        [HttpGet]
        [Authorize(Roles = "eventmanager")]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Event/{id}");
            string contents = await response.Content.ReadAsStringAsync();

            _ = contents;

            return RedirectToAction(nameof(Index));
        }

        // доделать нормальное представление
        // GET: EventLayoutController/Create
        [Authorize(Roles = "eventmanager")]
        public async Task<ActionResult> Create()
        {
            // использует полную модель представления EventLayoutViewModel
            return View(new EventLayoutViewModel
            {
                TMEvent = new TMEventDto(),
                TMLayouts = await GetVenueLayoutNames(),
            });
        }

        // POST: EventLayoutController/Create
        [HttpPost]
        [Authorize(Roles = "eventmanager")]
        /////[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventLayoutViewModel obj)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Event", obj?.TMEvent);
            var contents = await response.Content.ReadAsStringAsync();

            // получение ответа от сервиса по созданию евента
            TMEventDto result = JsonConvert.DeserializeObject<TMEventDto>(contents);

            switch (result?.Status)
            {
                case TMEventStatus.Success:
                    return RedirectToAction(nameof(Index));
                    ////return RedirectToAction("SetSeveralPrice", "Purchase", new { idEvent = obj.TMEvent.Id });
                case TMEventStatus.DateInPast:
                    ModelState.AddModelError("", "date is in a past"); break;
                case TMEventStatus.DateWrongOrder:
                    ModelState.AddModelError("", "end date before start date"); break;
                case TMEventStatus.SameByDateObj:
                    ModelState.AddModelError("", "this venue is busy at this time anouther event"); break;
                default:
                    ModelState.AddModelError("", "something wrong"); break;
            }

            obj ??= new EventLayoutViewModel { TMEvent = new TMEventDto() };
            obj.TMLayouts = await GetVenueLayoutNames();

            return View(obj);
        }

        // GET: EventLayoutController/Edit/5
        [Authorize(Roles = "eventmanager")]
        public async Task<ActionResult> Edit(int id)
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
            if (tmevent == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new EventLayoutViewModel
            {
                TMEvent = tmevent,
                TMLayouts = await GetVenueLayoutNames(),
            });
        }

        // POST: EventLayoutController/Edit/5
        [HttpPost]
        [Authorize(Roles = "eventmanager")]
        ////[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventLayoutViewModel obj)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Event/{id}", obj?.TMEvent);
            var contents = await response.Content.ReadAsStringAsync();

            // получение ответа от сервиса по созданию евента
            TMEventDto result = JsonConvert.DeserializeObject<TMEventDto>(contents);

            switch (result?.Status)
            {
                case TMEventStatus.Success:
                    return RedirectToAction(nameof(Index));
                ////return RedirectToAction("SetSeveralPrice", "Purchase", new { idEvent = obj.TMEvent.Id });
                case TMEventStatus.DateInPast:
                    ModelState.AddModelError("", "date is in a past"); break;
                case TMEventStatus.DateWrongOrder:
                    ModelState.AddModelError("", "end date before start date"); break;
                case TMEventStatus.SameByDateObj:
                    ModelState.AddModelError("", "this venue is busy at this time anouther event"); break;
                default:
                    ModelState.AddModelError("", "something wrong =("); break;
            }

            if (obj == null)
            {
                var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
                if (tmevent == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                obj = new EventLayoutViewModel
                {
                    TMEvent = tmevent,
                    TMLayouts = await GetVenueLayoutNames(), // странная фигня при смене layout-а
                };
            }

            return View(obj);
        }

        private async Task<List<SelectListItem>> GetVenueLayoutNames()
        {
            CultureInfo cultures = CultureInfo.CreateSpecificCulture("en-US");

            // получаем все возможные лайауты для преедачи в представление
            var layouts = await _httpClient.GetFromJsonAsync<List<TMLayoutDto>>("api/Layout");

            var items = new List<SelectListItem>();
            layouts.ForEach(l => items.Add(new SelectListItem
            {
                Text = l.Id + "--" + l.Description,
                Value = l.Id.ToString("G", cultures),
            }));

            return items;
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
