using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Domain.DTO;

namespace TicketManagement.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase, IDisposable
    {
        private bool _disposed;
        private HttpClient _httpClient;

        public EventController()
        {
            _disposed = false;
            _httpClient = new HttpClient();

#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: api/<EventController>
        [HttpGet]
        public async Task<List<TMEventDto>> Get() //// +
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Event");
            return tmevents;
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public async Task<TMEventDto> Get(int id) //// +
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
            return tmevent;
        }

        // POST api/<EventController>
        [HttpPost]
        public async void Post([FromBody] TMEventDto obj)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Event", obj);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            ////return response.Headers.Location;
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] TMEventDto obj)
        {
            // значение id отправляется в строке запроса. obj - в теле
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Event/{id}", obj);
            response.EnsureSuccessStatusCode();

            // десериализуем и обновляем объект
            ////obj = await response.Content.ReadAsAsync<TMEventDto>();
            ////return obj;
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Event/{id}");
            response.EnsureSuccessStatusCode();
            ////return response.StatusCode;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing && _httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
