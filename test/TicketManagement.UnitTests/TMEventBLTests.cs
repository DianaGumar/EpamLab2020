using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.UnitTests
{
    public class TMEventBLTests
    {
        [Test]
        public void CreateEventWithoutSeats()
        {
            Mock<ITMEventService> mockTMEventService = new Mock<ITMEventService>();
            Mock<IAreaService> mockAreaService = new Mock<IAreaService>();
            Mock<ISeatService> mockSeatService = new Mock<ISeatService>();

            TMEvent tmeventPre = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            TMEvent tmeventPostReal = new TMEvent
            {
                Id = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            mockSeatService.Setup(m => m.GetAllSeat()).Returns(new List<Seat>());
            mockAreaService.Setup(m => m.GetAllArea())
                .Returns(new List<Area> { new Area { Id = 1, TMLayoutId = tmeventPre.TMLayoutId } });
            mockTMEventService.Setup(m => m.CreateTMEvent(tmeventPre)).Returns(tmeventPostReal);

            ITMEventBL tmeventBL = new TMEventBL(mockTMEventService.Object, mockAreaService.Object, mockSeatService.Object);

            TMEvent tmeventPost = tmeventBL.CreateTMEvent(tmeventPre);

            tmeventPost.Should().NotBeEquivalentTo(tmeventPostReal);
            tmeventPost.Should().BeEquivalentTo(tmeventPre);
        }

        [Test]
        public void CreateEventWithSeats()
        {
            Mock<ITMEventService> mockTMEventService = new Mock<ITMEventService>();
            Mock<IAreaService> mockAreaService = new Mock<IAreaService>();
            Mock<ISeatService> mockSeatService = new Mock<ISeatService>();

            TMEvent tmeventPre = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            TMEvent tmeventPostReal = new TMEvent
            {
                Id = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            mockSeatService.Setup(m => m.GetAllSeat()).Returns(new List<Seat> { new Seat { AreaId = 1 } });
            mockAreaService.Setup(m => m.GetAllArea())
                .Returns(new List<Area> { new Area { Id = 1, TMLayoutId = tmeventPre.TMLayoutId } });
            mockTMEventService.Setup(m => m.CreateTMEvent(tmeventPre)).Returns(tmeventPostReal);

            ITMEventBL tmeventBL = new TMEventBL(mockTMEventService.Object, mockAreaService.Object, mockSeatService.Object);

            TMEvent tmeventPost = tmeventBL.CreateTMEvent(tmeventPre);

            tmeventPost.Should().BeEquivalentTo(tmeventPostReal);
        }
    }
}
