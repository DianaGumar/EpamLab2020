using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Models
{
    public class PurchaseHistoryViewModel
    {
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Seat row")]
        public string SeatRow { get; set; }

        [Display(Name = "Seat number")]
        public string SeatNumber { get; set; }

        public string Cost { get; set; }

        [Display(Name = "Booking Date")]
        public string BookingDate { get; set; }

        public bool EventLast { get; set; }
    }

    public class TMEventViewModel
    {
        public TMEventDto TMEvent { get; set; }

        public List<System.Web.Mvc.SelectListItem> TMLayouts { get; internal set; }
    }

    public class TMLayoutVenueViewModel
    {
        public TMLayoutDto TMLayout { get; set; }

        public string VenueName { get; set; }

        public List<System.Web.Mvc.SelectListItem> Venues { get; internal set; }
    }

    public class TMLayoutViewModel
    {
        public TMLayoutDto Layout { get; set; }

        public List<AreaViewModel> Areas { get; internal set; }
    }

    public class AreaViewModel
    {
        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public int CountSeatsY { get; set; }

        public int CountSeatsX { get; set; }

        public string CoordXStr { get; set; }

        public string CoordYStr { get; set; }

        public string CountSeatsYStr { get; set; }

        public string CountSeatsXStr { get; set; }

        public List<SeatViewModel> Seats { get; internal set; }
    }

    public class SeatViewModel
    {
        public int Row { get; set; }

        public int Number { get; set; }

        public string NumberStr { get; set; }

        public string RowStr { get; set; }
    }
}