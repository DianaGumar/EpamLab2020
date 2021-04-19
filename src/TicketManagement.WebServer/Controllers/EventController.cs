using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // +
        public async Task<List<TMEventDto>> Index()
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Events");
            return tmevents;
        }

        // +
        public async Task<TMEventDto> Details(int id)
        {
            ////            HttpResponseMessage result = await httpClient.GetAsync("api/Events/1003");
            ////            result.EnsureSuccessStatusCode();
            ////            if (result.IsSuccessStatusCode)
            ////                tmevent = await result.Content.ReadAsAsync<TMEventDto>();

            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Events/{id}");
            return tmevent;
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public async Task<Uri> Create(TMEventDto obj)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Events", obj);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        [HttpPut]
        ////[ValidateAntiForgeryToken]
        public async Task<TMEventDto> Edit(TMEventDto obj)
        {
            // значение id отправляется в строке запроса. obj - в теле
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Events/{obj?.Id}", obj);
            response.EnsureSuccessStatusCode();

            // десериализуем и обновляем объект
            obj = await response.Content.ReadAsAsync<TMEventDto>();

            return obj;
        }

        // +
        [HttpDelete]
        public async Task<HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Events/{id}");

            return response.StatusCode;
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
