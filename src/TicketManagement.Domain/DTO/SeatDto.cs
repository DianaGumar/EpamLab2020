namespace TicketManagement.Domain.DTO
{
    public enum SeatState
    {
        Free,
        Busy,
    }

    // value obj ?
    public class SeatDto
    {
        public int Id { get; set; }

        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}
