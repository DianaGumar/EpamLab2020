using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.IntegrationTests
{
    // with real data base
    [TestFixture]
    public class DALRepositoryTests
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        [Test]
        public void RepositoryCreateReadMethodByVenueTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

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
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

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
            new Venue().Should().BeEquivalentTo(venueRepository.GetById(venue.Id));
            new TMLayout().Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
            new Area().Should().BeEquivalentTo(areaRepository.GetById(area.Id));
            new Seat().Should().BeEquivalentTo(seatRepository.GetById(seat.Id));
        }

        [Test]
        public void RepositoryUpdateMethodByVenueTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

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
    }
}
