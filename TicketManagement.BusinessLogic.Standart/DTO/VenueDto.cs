namespace TicketManagement.Domain.DTO
{
    public class VenueDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        // in meters
        public int Weidth { get; set; }

        public int Lenght { get; set; }
    }
}
