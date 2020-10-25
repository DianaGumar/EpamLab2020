namespace TicketManagement.Domain
{
    public class VenueModels
    {
        public int VenueId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        // in meters
        public int Weidth { get; set; }

        public int Lenght { get; set; }
    }
}
