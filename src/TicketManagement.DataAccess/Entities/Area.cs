using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    [Table("Area")]
    public class Area : IEntity
    {
        [Key]
        public int Id { get; set; }

        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int TMLayoutId { get; set; }

        [ForeignKey("TMLayoutId")]
        private TMLayout TMLayout { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }
    }
}
