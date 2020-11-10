using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    // renamed by code controle
    [Table("TMEvent")]
    public class TMEvent : IEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(120)]
        [Required]
        public string Name { get; set; }

        [MaxLength(int.MaxValue)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int TMLayoutId { get; set; }

        [ForeignKey("TMLayoutId")]
        private TMLayout TMLayout { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartEvent { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndEvent { get; set; }

        [MaxLength(int.MaxValue)]
        public string Img { get; set; }
    }
}
