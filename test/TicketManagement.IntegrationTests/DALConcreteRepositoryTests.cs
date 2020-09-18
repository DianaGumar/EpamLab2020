using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.IntegrationTests
{
    public class DALConcreteRepositoryTests
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        [Test]
        public void TMEventAreaRepositoryAndEventSeatRepositoryCreateMethodTest()
        {
            // arange
            ITMEventAreaRepository eventAreaRepository = new TMEventAreaRepository(_str);
            ITMEventRepository eventRepository = new TMEventRepository(_str);
            ITMEventSeatRepository eventSeatRepository = new TMEventSeatRepository(_str);

            int tmeventId = 1;

            // act
            TMEvent tmevent = eventRepository.GetById(tmeventId);
            if (tmevent.Id != 0)
            {
                TMEventArea eventArea = eventAreaRepository.Create(
                    new TMEventArea { Description = "some d", TMEventId = tmeventId, CoordX = 0, CoordY = 0, Price = 0 });
                TMEventSeat eventSeat = eventSeatRepository.Create(
                    new TMEventSeat { TMEventAreaId = eventArea.Id, Number = 1, Row = 1, State = 0 });

                // assert
                eventArea.Should().BeEquivalentTo(eventAreaRepository.GetById(eventArea.Id));
                eventSeat.Should().BeEquivalentTo(eventSeatRepository.GetById(eventSeat.Id));

                eventSeatRepository.Remove(eventSeat);
                eventAreaRepository.Remove(eventArea);
            }
        }
    }
}
