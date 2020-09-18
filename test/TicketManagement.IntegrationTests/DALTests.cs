using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.IntegrationTests
{
    // with real data base
    [TestFixture]
    public class DALTests
    {
        private readonly string _str =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=TicketManagement.Database;Integrated Security=True";

        [Test]
        public void RepositoryCreateMethodTest()
        {
            // arange
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            // act
            TMLayout layout = layoutRepository.Create(
                new TMLayout { Description = "some desc", VenueId = 2 });
            Area area = areaRepository.Create(
                new Area { Description = "area1", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id });
            Seat seat = seatRepository.Create(
                new Seat { Number = 1, Row = 1, AreaId = area.Id });

            // assert
            layout.Should().BeEquivalentTo(layoutRepository.GetById(layout.Id));
            area.Should().BeEquivalentTo(areaRepository.GetById(area.Id));
            seat.Should().BeEquivalentTo(seatRepository.GetById(seat.Id));
        }
    }
}
