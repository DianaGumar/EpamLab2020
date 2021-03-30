using System.Collections.Generic;

namespace TicketManagement.Domain.DTO
{
    public class TMEventAreaDto
    {
        public int Id { get; set; }

        public int TMEventId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public int CountSeatsY { get; set; }

        public int CountSeatsX { get; set; }

        public IEnumerable<TMEventSeatDto> TMEventSeats { get; set; }

        public decimal Price { get; set; }
    }
}
