using System;

namespace TicketManagement.Domain.DTO
{
    public class PurchaseHistoryDto
    {
        public TMEventDto TMEventObj { get; set; }

        public TMEventSeatDto SeatObj { get; set; }

        public decimal AreaPrice { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
