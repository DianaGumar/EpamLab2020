namespace TicketManagement.Domain.DTO
{
    // value obj ?
    public class SeatDto
    {
        public int Id { get; set; }

        public AreaDto Area { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}
