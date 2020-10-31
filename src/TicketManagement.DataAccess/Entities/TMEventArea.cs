namespace TicketManagement.DataAccess.Entities
{
    public class TMEventArea
    {
        public int Id { get; set; }

        public int TMEventId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public decimal Price { get; set; }
    }
}
