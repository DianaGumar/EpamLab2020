namespace TicketManagement.DataAccess.Model
{
    public class Area
    {
        public int Id { get; set; }

        public int TMLayoutId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }
    }
}
