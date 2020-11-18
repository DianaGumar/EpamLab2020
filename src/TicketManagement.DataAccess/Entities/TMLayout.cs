using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("TMLayout")]
    public class TMLayout : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VenueId { get; set; }

        [ForeignKey("VenueId")]
        private Venue Venue { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
    }
}
