using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TMLayoutId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }
    }
}
