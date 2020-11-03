namespace TicketManagement.Domain.DTO
{
    public class AreaDto
    {
        public int Id { get; set; }

        public int TMLayoutId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        // public IEnumerable<SeatDto> Seats
        public int CoordY { get; set; }
    }
}
