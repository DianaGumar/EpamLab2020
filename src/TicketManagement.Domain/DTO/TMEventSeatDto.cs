﻿namespace TicketManagement.Domain.DTO
{
    public class TMEventSeatDto
    {
        public int Id { get; set; }

        public TMEventAreaDto TMEventArea { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatState State { get; set; }

        public string NumberStr { get; set; }

        public string RowStr { get; set; }
    }
}
