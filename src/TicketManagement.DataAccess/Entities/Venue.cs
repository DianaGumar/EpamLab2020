using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Venue")]
    public class Venue : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }

        // in meters
        [Required]
        public int Weidth { get; set; }

        [Required]
        public int Lenght { get; set; }
    }
}
