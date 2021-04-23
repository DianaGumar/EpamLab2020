////using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.Domain.DTO;

namespace TicketManagement.WebClient.Models
{
    public class EventLayoutViewModel
    {
        public TMEventDto TMEvent { get; set; }

        public SelectList TMLayouts { get; internal set; }
    }
}
