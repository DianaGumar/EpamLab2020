using System.Configuration;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.DataAccess.DAL;

namespace TicketManagement.DataAccess
{
    public class TMContext : DbContext
    {
        ////public TMContext(DbContextOptions<TMContext> options)
        ////    : base(options)
        ////{
        ////}

        ////public DbSet<MigrationHistory> MigrationHistory { get; set; }
        ////public DbSet<AspNetRoles> AspNetRoles { get; set; }
        ////public DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        ////public DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        ////public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        ////public DbSet<AspNetUsers> AspNetUsers { get; set; }

        public DbSet<TMUser> TMUsers { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<TMEvent> TMEvents { get; set; }

        public DbSet<TMEventArea> TMEventAreas { get; set; }

        public DbSet<TMEventSeat> TMEventSeats { get; set; }

        public DbSet<TMLayout> TMLayouts { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder?.Entity<TopTMEventId>().HasNoKey();
            modelBuilder?.Entity<CountRow>().HasNoKey();

            // off ef cascade deleting
            ////modelBuilder?.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // решить проблемму с мапингом хранимых процедур
            // maping stored prosedures to ef CUD operations with TMEvent
            ////modelBuilder?.Entity<TMEvent>()
            ////  .MapToStoredProcedures(s =>
            ////    s.Update(u => u.HasName("TMEvent_Update")
            ////                   .Parameter(b => b.Id, "Id")
            ////                   .Parameter(b => b.Name, "Name")
            ////                   .Parameter(b => b.Description, "Description")
            ////                   .Parameter(b => b.TMLayoutId, "LayoutId")
            ////                   .Parameter(b => b.StartEvent, "StartEvent")
            ////                   .Parameter(b => b.EndEvent, "EndEvent")
            ////                   .Parameter(b => b.Img, "Img"))
            ////     .Delete(d => d.HasName("TMEvent_Delete")
            ////                   .Parameter(b => b.Id, "Id"))
            ////     .Insert(i => i.HasName("TMEvent_Create")
            ////                   .Parameter(b => b.Name, "Name")
            ////                   .Parameter(b => b.Description, "Description")
            ////                   .Parameter(b => b.TMLayoutId, "TMLayoutId")
            ////                   .Parameter(b => b.StartEvent, "StartEvent")
            ////                   .Parameter(b => b.EndEvent, "EndEvent")
            ////                   .Parameter(b => b.Img, "Img")
            ////                   .Result(b => b.Id, "Id")));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////var contextOptions = new DbContextOptionsBuilder<TMContext>()
            ////    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
            ////    .Options;

            optionsBuilder.UseSqlServer(ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
