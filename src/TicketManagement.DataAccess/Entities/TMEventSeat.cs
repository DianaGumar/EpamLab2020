namespace TicketManagement.DataAccess.Entities
{
    public class TMEventSeat
    {
        public int Id { get; set; }

        public int TMEventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public int State { get; set; }
    }
}
