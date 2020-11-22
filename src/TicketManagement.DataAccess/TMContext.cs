using System.Data.Entity;
////using System.Data.Entity.ModelConfiguration.Conventions;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess
{
    public class TMContext : DbContext
    {
        public TMContext()
            : base("DefaultConnection")
        {
        }

        public TMContext(string dbName)
            : base(dbName)
        {
        }

        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        public DbSet<TMUser> Users { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<TMEvent> TMEvents { get; set; }

        public DbSet<TMEventArea> TMEventAreas { get; set; }

        public DbSet<TMEventSeat> TMEventSeats { get; set; }

        public DbSet<TMLayout> TMLayouts { get; set; }

        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // off ef cascade deleting
            ////modelBuilder?.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // maping stored prosedures to ef CUD operations with TMEvent
            modelBuilder?.Entity<TMEvent>()
              .MapToStoredProcedures(s =>
                s.Update(u => u.HasName("TMEvent_Update")
                               .Parameter(b => b.Id, "Id")
                               .Parameter(b => b.Name, "Name")
                               .Parameter(b => b.Description, "Description")
                               .Parameter(b => b.TMLayoutId, "LayoutId")
                               .Parameter(b => b.StartEvent, "StartEvent")
                               .Parameter(b => b.EndEvent, "EndEvent")
                               .Parameter(b => b.Img, "Img"))
                 .Delete(d => d.HasName("TMEvent_Delete")
                               .Parameter(b => b.Id, "Id"))
                 .Insert(i => i.HasName("TMEvent_Create")
                               .Parameter(b => b.Name, "Name")
                               .Parameter(b => b.Description, "Description")
                               .Parameter(b => b.TMLayoutId, "TMLayoutId")
                               .Parameter(b => b.StartEvent, "StartEvent")
                               .Parameter(b => b.EndEvent, "EndEvent")
                               .Parameter(b => b.Img, "Img")
                               .Result(b => b.Id, "Id")));

            base.OnModelCreating(modelBuilder);
        }
    }
}
