using System;
////using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.DataAccess;
using TicketManagement.DataAccess.DAL;

namespace TicketManagement.IntegrationTests
{
    // with real data base
    [TestFixture]
    public class DALRepositoryTests : IDisposable
    {
        ////private readonly string _str = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        private readonly TMContext _context = new TMContext();
        private bool _isDisposed;

        [Test]
        public void RepositoryCreateReadMethodByVenueTest()
        {
            // arange
            var venueRepository = new VenueRepositoryEF(_context);
            var layoutRepository = new TMLayoutRepositoryEF(_context);
            var areaRepository = new AreaRepositoryEF(_context);
            var seatRepository = new SeatRepositoryEF(_context);

            // act
            Venue venue = venueRepository.Create(
                new Venue { Description = "some v desc", Address = "some address", Lenght = 2, Weidth = 2 });
            TMLayout layout = layoutRepository.Create(
            new TMLayout { Description = "some desc", VenueId = venue.Id });
            Area area = areaRepository.Create(
                new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
            Seat seat = seatRepository.Create(
                new Seat { Number = 1, Row = 1, AreaId = area.Id });

            // assert
            venue.Should().BeEquivalentTo(venueRepository.GetById(venue.Id));
            layout.Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
            area.Should().BeEquivalentTo(areaRepository.GetById(area.Id));
            seat.Should().BeEquivalentTo(seatRepository.GetById(seat.Id));

            seatRepository.Remove(seat.Id);
            areaRepository.Remove(area);
            layoutRepository.Remove(layout);
            venueRepository.Remove(venue.Id);
        }

        [Test]
        public void RepositoryDeleteMethodByVenueTest()
        {
            // arange
            var venueRepository = new VenueRepositoryEF(_context);
            var layoutRepository = new TMLayoutRepositoryEF(_context);
            var areaRepository = new AreaRepositoryEF(_context);
            var seatRepository = new SeatRepositoryEF(_context);

            // act
            Venue venue = venueRepository.Create(
            new Venue { Description = "some v desc", Address = "some address", Lenght = 2, Weidth = 2, Phone = "some phone" });
            TMLayout layout = layoutRepository.Create(
            new TMLayout { Description = "some desc", VenueId = venue.Id });
            Area area = areaRepository.Create(
                new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
            Seat seat = seatRepository.Create(
                new Seat { Number = 1, Row = 1, AreaId = area.Id });

            seatRepository.Remove(seat);
            areaRepository.Remove(area);
            layoutRepository.Remove(layout);
            venueRepository.Remove(venue.Id);

            // assert
            venueRepository.GetById(venue.Id).Should().BeNull();
            layoutRepository.GetById(layout.Id).Should().BeNull();
            areaRepository.GetById(area.Id).Should().BeNull();
            seatRepository.GetById(seat.Id).Should().BeNull();
        }

        [Test]
        public void RepositoryUpdateMethodByVenueTest()
        {
            // arange
            var venueRepository = new VenueRepositoryEF(_context);
            var layoutRepository = new TMLayoutRepositoryEF(_context);
            var areaRepository = new AreaRepositoryEF(_context);
            var seatRepository = new SeatRepositoryEF(_context);

            // act
            Venue venue = venueRepository.Create(
            new Venue { Description = "some v desc", Address = "some address", Lenght = 2, Weidth = 2, Phone = "some phone" });
            TMLayout layout = layoutRepository.Create(
            new TMLayout { Description = "some desc", VenueId = venue.Id });
            Area area = areaRepository.Create(
                new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
            Seat seat = seatRepository.Create(
                new Seat { Number = 1, Row = 1, AreaId = area.Id });

            venue.Description = "new v desc";
            layout.Description = "new desc";
            area.Description = "new desc";
            seat.Number += 1;

            venueRepository.Update(venue);
            layoutRepository.Update(layout);
            areaRepository.Update(area);
            seatRepository.Update(seat);

            // assert
            venue.Should().BeEquivalentTo(venueRepository.GetById(venue.Id));
            layout.Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
            area.Should().BeEquivalentTo(areaRepository.GetById(area.Id));
            seat.Should().BeEquivalentTo(seatRepository.GetById(seat.Id));

            seatRepository.Remove(seat);
            areaRepository.Remove(area);
            layoutRepository.Remove(layout);
            venueRepository.Remove(venue.Id);
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // free managed resources
                _context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
