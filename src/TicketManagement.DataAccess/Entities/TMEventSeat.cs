using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("TMEventSeat")]
    public class TMEventSeat : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TMEventAreaId { get; set; }

        [ForeignKey("TMEventAreaId")]
        private TMEventArea TMEventArea { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int State { get; set; }
    }
}
