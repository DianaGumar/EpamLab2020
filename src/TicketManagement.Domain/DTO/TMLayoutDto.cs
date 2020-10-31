namespace TicketManagement.Domain.DTO
{
    public class TMLayoutDto
    {
        public int Id { get; set; }

        public VenueDto Venue { get; set; }

        // public IEnumerable<AreaDto> Areas
        public string Description { get; set; }
    }
}
