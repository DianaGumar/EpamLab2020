using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess
{
    public class TMContext : DbContext
    {
        public TMContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<TMEvent> TMEvents { get; set; }

        public DbSet<TMEventArea> TMEventAreas { get; set; }

        public DbSet<TMEventSeat> TMEventSeats { get; set; }

        public DbSet<TMLayout> TMLayouts { get; set; }

        public DbSet<Venue> Venues { get; set; }
    }
}
