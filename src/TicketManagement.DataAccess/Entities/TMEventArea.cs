using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("TMEventArea")]
    public class TMEventArea : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TMEventId { get; set; }

        [ForeignKey("TMEventId")]
        private TMEvent TMEvent { get; set; }

        [MaxLength(200)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
