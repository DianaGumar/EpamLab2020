using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.UnitTests
{
    public class TMEventBLTests
    {
        [Test]
        public void CreateEventWithoutSeats()
        {
            var mockTMEventRepository = new Mock<ITMEventRepository>();
            var mockAreaService = new Mock<ITMEventAreaService>();
            var mockSeatService = new Mock<ITMEventSeatService>();

            var tmeventPre = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPre_dal = new TMEventDto
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal = new TMEvent
            {
                Id = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            mockSeatService.Setup(m => m.GetAllTMEventSeat()).Returns(new List<TMEventSeatDto>());
            mockAreaService.Setup(m => m.GetAllTMEventArea())
                .Returns(new List<TMEventAreaDto> { new TMEventAreaDto { Id = 1, TMEventId = tmeventPre.Id } });
            mockTMEventRepository.Setup(m => m.Create(tmeventPre)).Returns(tmeventPostReal);

            ITMEventService tmeventBL = new TMEventService(
                mockTMEventRepository.Object,
                mockAreaService.Object,
                mockSeatService.Object);

            TMEventDto tmeventPost = tmeventBL.CreateTMEvent(tmeventPre_dal);

            tmeventPost.Should().NotBeEquivalentTo(tmeventPostReal);
            tmeventPost.Should().BeEquivalentTo(tmeventPre);
        }

        [Test]
        public void CreateEventWithSeats()
        {
            var mockTMEventRepository = new Mock<ITMEventRepository>();
            var mockAreaService = new Mock<ITMEventAreaService>();
            var mockSeatService = new Mock<ITMEventSeatService>();

            var tmeventPre = new TMEvent
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPostReal = new TMEvent
            {
                Id = 1,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            var tmeventPre_dal = new TMEventDto
            {
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
                TMLayoutId = 1,
            };

            mockSeatService.Setup(m => m.GetAllTMEventSeat()).Returns(new List<TMEventSeatDto>
            {
                new TMEventSeatDto { TMEventAreaId = 1 },
            });
            mockAreaService.Setup(m => m.GetAllTMEventArea())
                .Returns(new List<TMEventAreaDto> { new TMEventAreaDto { Id = 1, TMEventId = tmeventPre.Id } });
            mockTMEventRepository.Setup(m => m.Create(tmeventPre)).Returns(tmeventPostReal);

            var tmeventBL = new TMEventService(
                mockTMEventRepository.Object,
                mockAreaService.Object,
                mockSeatService.Object);

            TMEventDto tmeventPost = tmeventBL.CreateTMEvent(tmeventPre_dal);

            tmeventPost.Should().BeEquivalentTo(tmeventPostReal);
        }
    }
}
