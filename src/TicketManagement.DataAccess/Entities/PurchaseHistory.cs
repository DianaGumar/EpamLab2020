using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    public class PurchaseHistory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        [Required]
        public string UserId { get; set; }

        [ForeignKey("TMEventSeatId")]
        private TMEventSeat TMEventSeat { get; set; }

        public int TMEventSeatId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime BookingDate { get; set; }
    }
}
