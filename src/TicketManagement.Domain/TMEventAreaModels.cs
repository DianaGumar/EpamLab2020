namespace TicketManagement.Domain
{
    public class TMEventAreaModels
    {
        public int TMEventAreaId { get; set; }

        public int TMEventId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public int CountSeatsY { get; set; }

        public int CountSeatsX { get; set; }

        public string CoordXStr { get; set; }

        public string CoordYStr { get; set; }

        public string CountSeatsYStr { get; set; }

        public string CountSeatsXStr { get; set; }

        public decimal Price { get; set; }
    }
}
