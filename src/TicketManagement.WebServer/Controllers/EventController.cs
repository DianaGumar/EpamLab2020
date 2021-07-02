using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            _httpClient.BaseAddress = new Uri("https://localhost:5041/");
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: api/<EventController>
        [HttpGet("all")]
        [Authorize]
        public async Task<List<TMEventDto>> GetAll()
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Event/existing-events");
            return tmevents;
        }

        // get all relevant events
        // GET: api/<EventController>
        [HttpGet]
        public async Task<List<TMEventDto>> Get()
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMEventDto>>("api/Event");
            return tmevents;
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "eventmanager")]
        public async Task<TMEventDto> Get(int id) //// +
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMEventDto>($"api/Event/{id}");
            return tmevent;
        }

        // POST api/<EventController>
        [HttpPost]
        public async Task<TMEventDto> Post([FromBody] TMEventDto obj)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<TMEventDto>("api/Event", obj);
            var contents = await response.Content.ReadAsStringAsync();

            // получение ответа от сервиса по созданию евента
            TMEventDto result = JsonConvert.DeserializeObject<TMEventDto>(contents);

            return result;
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "eventmanager")]
        public async Task<TMEventDto> Put(int id, [FromBody] TMEventDto obj)
        {
            // значение id отправляется в строке запроса. obj - в теле
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<TMEventDto>($"api/Event/{id}", obj);
            var contents = await response.Content.ReadAsStringAsync();

            // десериализуем и обновляем объект
            TMEventDto result = JsonConvert.DeserializeObject<TMEventDto>(contents);

            return result;
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "eventmanager")]
        public async Task<string> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Event/{id}");
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
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
