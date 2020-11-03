using System;

namespace TicketManagement.Domain.DTO
{
    public class TMEventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public int TMLayoutId { get; set; }

        public DateTime StartEvent { get; set; }

        public DateTime EndEvent { get; set; }

        public int AllSeats { get; set; }

        public int BusySeats { get; set; }

        // public IEnumerable<TMEventAreaDto> TMEventAreas
        public decimal MiddlePriceBySeat { get; set; }
    }
}
