using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Domain.DTO
{
    public class TMEventDto
    {
        public int Id { get; set; }

        [MaxLength(120)]
        [Required]
        public string Name { get; set; }

        [MaxLength(int.MaxValue)]
        public string Img { get; set; }

        [MaxLength(int.MaxValue)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int TMLayoutId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartEvent { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndEvent { get; set; }

        public int AllSeats { get; set; }

        public int BusySeats { get; set; }

        // public IEnumerable<TMEventAreaDto> TMEventAreas
        public decimal MiddlePriceBySeat { get; set; }
    }
}
