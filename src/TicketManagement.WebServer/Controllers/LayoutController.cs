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
    public class LayoutController : ControllerBase, IDisposable
    {
        private bool _disposed;
        private HttpClient _httpClient;

        public LayoutController()
        {
            _disposed = false;
            _httpClient = new HttpClient();

            // подключается к venue api
#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5031/");
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: api/<LayoutController>
        [HttpGet]
        public async Task<List<TMLayoutDto>> Get()
        {
            var tmevents = await _httpClient.GetFromJsonAsync<List<TMLayoutDto>>("api/Layout");
            return tmevents;
        }

        // GET api/<LayoutController>/5
        [HttpGet("{id}")]
        public async Task<TMLayoutDto> Get(int id)
        {
            var tmevent = await _httpClient.GetFromJsonAsync<TMLayoutDto>($"api/Layout/{id}");
            return tmevent;
        }

        ////// POST api/<LayoutController>
        ////[HttpPost]
        ////public void Post([FromBody] string value)
        ////{
        ////}

        ////// PUT api/<LayoutController>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        ////// DELETE api/<LayoutController>/5
        ////[HttpDelete("{id}")]
        ////public void Delete(int id)
        ////{
        ////}

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
