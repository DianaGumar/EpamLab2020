namespace TicketManagement.Domain
{
    public enum SeatState
    {
        Free,
        Busy,
    }

    public class TMEventSeatModels
    {
        public int Id { get; set; }

        public int TMEventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatState State { get; set; }

        public string NumberStr { get; set; }

        public string RowStr { get; set; }
    }
}
