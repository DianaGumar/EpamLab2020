using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Models
{
    public class TMEventViewModel
    {
        public TMEventDto TMEvent { get; set; }

        public List<System.Web.Mvc.SelectListItem> TMLayouts { get; internal set; }
    }

    public class TMLayoutVievModel
    {
        public TMLayoutDto TMLayout { get; set; }

        public string VenueName { get; set; }

        public List<System.Web.Mvc.SelectListItem> Venues { get; internal set; }
    }
}