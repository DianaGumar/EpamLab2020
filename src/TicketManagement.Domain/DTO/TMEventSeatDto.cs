namespace TicketManagement.Domain.DTO
{
    public class TMEventSeatDto
    {
        public int Id { get; set; }

        public int TMEventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatState State { get; set; }
    }
}
