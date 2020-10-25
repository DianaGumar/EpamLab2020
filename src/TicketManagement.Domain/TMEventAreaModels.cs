namespace TicketManagement.Domain
{
    public class TMEventAreaModels
    {
        public int TMEventAreaId { get; set; }

        public int TMEventId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public decimal Price { get; set; }
    }
}
