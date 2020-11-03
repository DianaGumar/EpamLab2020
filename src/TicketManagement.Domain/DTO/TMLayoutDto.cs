namespace TicketManagement.Domain.DTO
{
    public class TMLayoutDto
    {
        public int Id { get; set; }

        public int VenueId { get; set; }

        // public IEnumerable<AreaDto> Areas
        public string Description { get; set; }
    }
}
