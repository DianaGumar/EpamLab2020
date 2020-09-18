using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.IntegrationTests
{
    // with real data base
    [TestFixture]
    public class DALRepositoryTests
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        [Test]
        public void RepositoryCreateReadMethodByLayoutTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            int venueId = 1;

            // act
            Venue venue = venueRepository.GetById(venueId);
            if (venue.Id != 0)
            {
                TMLayout layout = layoutRepository.Create(
                new TMLayout { Description = "some desc", VenueId = venueId });
                Area area = areaRepository.Create(
                    new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
                Seat seat = seatRepository.Create(
                    new Seat { Number = 1, Row = 1, AreaId = area.Id });

                // assert
                layout.Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
                area.Should().BeEquivalentTo(areaRepository.GetById(area.Id));
                seat.Should().BeEquivalentTo(seatRepository.GetById(seat.Id));

                seatRepository.Remove(seat);
                areaRepository.Remove(area);
                layoutRepository.Remove(layout);
            }
        }

        [Test]
        public void RepositoryDeleteMethodByLayoutTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            int venueId = 1;

            // act
            Venue venue = venueRepository.GetById(venueId);
            if (venue.Id != 0)
            {
                TMLayout layout = layoutRepository.Create(
                new TMLayout { Description = "some desc", VenueId = venueId });
                Area area = areaRepository.Create(
                    new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
                Seat seat = seatRepository.Create(
                    new Seat { Number = 1, Row = 1, AreaId = area.Id });

                seatRepository.Remove(seat);
                areaRepository.Remove(area);
                layoutRepository.Remove(layout);

                // assert
                new TMLayout().Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
                new Area().Should().BeEquivalentTo(areaRepository.GetById(area.Id));
                new Seat().Should().BeEquivalentTo(seatRepository.GetById(seat.Id));
            }
        }

        [Test]
        public void RepositoryUpdateMethodByLayoutTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            int venueId = 1;

            // act
            Venue venue = venueRepository.GetById(venueId);
            if (venue.Id != 0)
            {
                TMLayout layout = layoutRepository.Create(
                new TMLayout { Description = "some desc", VenueId = venueId });
                Area area = areaRepository.Create(
                    new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
                Seat seat = seatRepository.Create(
                    new Seat { Number = 1, Row = 1, AreaId = area.Id });

                layout.Description = "new desc";
                area.Description = "new desc";
                seat.Number += 1;

                layoutRepository.Update(layout);
                areaRepository.Update(area);
                seatRepository.Update(seat);

                // assert
                layout.Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
                area.Should().BeEquivalentTo(areaRepository.GetById(area.Id));
                seat.Should().BeEquivalentTo(seatRepository.GetById(seat.Id));

                seatRepository.Remove(seat);
                areaRepository.Remove(area);
                layoutRepository.Remove(layout);
            }
        }
    }
}
