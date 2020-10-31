using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.DataAccess.Entities
{
    // renamed by code controle
    public class TMEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Description { get; set; }

        public int TMLayoutId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartEvent { get; set; }

        public DateTime EndEvent { get; set; }

        public string Img { get; set; }
    }
}
