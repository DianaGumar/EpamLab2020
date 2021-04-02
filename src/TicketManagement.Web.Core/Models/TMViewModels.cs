using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.Domain.DTO;

namespace TicketManagement.Web.Core.Models
{
    public class TMEventAreaViewModel
    {
        public int Id { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public int CountSeatsY { get; set; }

        public int CountSeatsX { get; set; }

        public string CoordXStr { get; set; }

        public string CoordYStr { get; set; }

        public string CountSeatsYStr { get; set; }

        public string CountSeatsXStr { get; set; }

        public List<TMEventSeatViewModel> Seats { get; internal set; }
    }

    public class TMEventSeatIdViewModel
    {
        public int TMEventId { get; set; }

        [Display(Name = "Seats id")]
        public string SeatsId { get; set; }
    }

    public class TMEventSeatViewModel
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public string RowStr { get; set; }

        public string NumberStr { get; set; }

        public SeatState State { get; set; }

        public bool IsChousen { get; set; }

        public string Color { get; set; }
    }

    public class PurchaseHistoryViewModel
    {
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Seat id")]
        public string Id { get; set; }

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

        public List<SelectListItem> TMLayouts { get; internal set; }
    }

    public class TMLayoutVenueViewModel
    {
        public TMLayoutDto TMLayout { get; set; }

        public string VenueName { get; set; }

        public List<SelectListItem> Venues { get; internal set; }
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