using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Seat")]
    public class Seat : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AreaId { get; set; }

        [ForeignKey("AreaId")]
        private Area Area { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
