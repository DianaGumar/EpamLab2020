﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Venue")]
    public class Venue
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

    [Table("TMUser")]
    public class TMUser
    {
        [Key]
        [MaxLength(128)]
        public string UserId { get; set; }

        [MaxLength(256)]
        public string UserLastName { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }

    [Table("TMLayout")]
    public class TMLayout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VenueId { get; set; }

        [ForeignKey("VenueId")]
        private Venue Venue { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
    }

    [Table("TMEventSeat")]
    public class TMEventSeat
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

    [Table("TMEventArea")]
    public class TMEventArea
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

    // renamed by code controler
    [Table("TMEvent")]
    public class TMEvent
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

    [Table("PurchaseHistory")]
    public class PurchaseHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        private TMUser TMUser { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("TMEventSeatId")]
        private TMEventSeat TMEventSeat { get; set; }

        public int TMEventSeatId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime BookingDate { get; set; }
    }

    [Table("Seat")]
    public class Seat
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

    [Table("Area")]
    public class Area
    {
        [Key]
        public int Id { get; set; }

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
