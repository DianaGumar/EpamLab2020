using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain;

namespace TicketManagement.UnitTests
{
    public class TMEventBLTests
    {
        [Test]
        public void CreateEventWithoutSeats()
        {
            var mockTMEventService = new Mock<ITMEventService>();
            var mockAreaService = new Mock<IAreaService>();
            var mockSeatService = new Mock<ISeatService>();
            var mocktmeventSeatService = new Mock<ITMEventSeatService>();
            var mocktmeventAreaService = new Mock<ITMEventAreaService>();

            var tmeventPre = new TMEventModels
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPre_dal = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal = new TMEventModels
            {
                TMEventId = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal_dal = new TMEvent
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
            mockTMEventService.Setup(m => m.CreateTMEvent(tmeventPre_dal)).Returns(tmeventPostReal_dal);

            ITMEventBL tmeventBL = new TMEventBL(
                mockTMEventService.Object,
                mockAreaService.Object,
                mockSeatService.Object,
                mocktmeventAreaService.Object,
                mocktmeventSeatService.Object);

            TMEventModels tmeventPost = tmeventBL.CreateTMEvent(tmeventPre);

            tmeventPost.Should().NotBeEquivalentTo(tmeventPostReal);
            tmeventPost.Should().BeEquivalentTo(tmeventPre);
        }

        [Test]
        public void CreateEventWithSeats()
        {
            var mockTMEventService = new Mock<ITMEventService>();
            var mockAreaService = new Mock<IAreaService>();
            var mockSeatService = new Mock<ISeatService>();
            var mocktmeventSeatService = new Mock<ITMEventSeatService>();
            var mocktmeventAreaService = new Mock<ITMEventAreaService>();

            var tmeventPre = new TMEventModels
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal = new TMEventModels
            {
                TMEventId = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPre_dal = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal_dal = new TMEvent
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
            mockTMEventService.Setup(m => m.CreateTMEvent(tmeventPre_dal)).Returns(tmeventPostReal_dal);

            ITMEventBL tmeventBL = new TMEventBL(
                mockTMEventService.Object,
                mockAreaService.Object,
                mockSeatService.Object,
                mocktmeventAreaService.Object,
                mocktmeventSeatService.Object);

            TMEventModels tmeventPost = tmeventBL.CreateTMEvent(tmeventPre);

            tmeventPost.Should().BeEquivalentTo(tmeventPostReal);
        }
    }
}
