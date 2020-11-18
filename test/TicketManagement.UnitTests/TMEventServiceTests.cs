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
    public class TMEventServiceTests
    {
        [Test]
        public void CreateTMEventTestSameObject()
        {
            List<TMEvent> events = new List<TMEvent>();
            events.Add(new TMEvent
            {
                Id = 1,
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });
            events.Add(new TMEvent
            {
                Id = 2,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
            });

            List<TMEventDto> events_dto = new List<TMEventDto>();
            events_dto.Add(new TMEventDto
            {
                Id = 1,
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });
            events_dto.Add(new TMEventDto
            {
                Id = 2,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
            });

            Mock<ITMEventRepository> mockTMEventRepository = new Mock<ITMEventRepository>();
            mockTMEventRepository.Setup(x => x.GetAll()).Returns(events);

            Mock<ITMEventAreaService> mockTMEventAreaService = new Mock<ITMEventAreaService>();
            mockTMEventAreaService.Setup(x => x.GetAllTMEventArea())
                .Returns(new List<TMEventAreaDto> { new TMEventAreaDto { TMEventId = 1, Id = 1 } });

            Mock<ITMEventSeatService> mockTMEventSeatService = new Mock<ITMEventSeatService>();
            mockTMEventSeatService.Setup(x => x.GetAllTMEventSeat())
                .Returns(new List<TMEventSeatDto> { new TMEventSeatDto { TMEventAreaId = 1, Id = 1 } });

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object,
                mockTMEventAreaService.Object, mockTMEventSeatService.Object);

            TMEventDto tmevent = tmeventService.CreateTMEvent(new TMEventDto
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });

            tmevent.Should().BeEquivalentTo(events_dto[0]);
        }

        [Test]
        public void CreateTMEventTestDatePastValidation()
        {
            List<TMEvent> events = new List<TMEvent>();
            events.Add(new TMEvent
            {
                Id = 1,
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });
            events.Add(new TMEvent
            {
                Id = 2,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
            });

            Mock<ITMEventRepository> mockTMEventRepository = new Mock<ITMEventRepository>();
            mockTMEventRepository.Setup(x => x.GetAll()).Returns(events);

            Mock<ITMEventAreaService> mockTMEventAreaService = new Mock<ITMEventAreaService>();
            Mock<ITMEventSeatService> mockTMEventSeatService = new Mock<ITMEventSeatService>();

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object,
                 mockTMEventAreaService.Object, mockTMEventSeatService.Object);

            TMEventDto tmeventPre_dto = new TMEventDto
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(-1),
                EndEvent = DateTime.Now.Date,
            };

            TMEventDto tmeventPast = tmeventService.CreateTMEvent(tmeventPre_dto);

            tmeventPast.Should().BeEquivalentTo(tmeventPre_dto);
        }

        [Test]
        public void CreateTMEventTestDateDifferenseValidation()
        {
            List<TMEvent> events = new List<TMEvent>();
            events.Add(new TMEvent
            {
                Id = 1,
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });
            events.Add(new TMEvent
            {
                Id = 2,
                Name = "a2",
                Description = "d2",
                StartEvent = DateTime.Now.Date.AddDays(3),
                EndEvent = DateTime.Now.Date.AddDays(4),
            });

            Mock<ITMEventRepository> mockTMEventRepository = new Mock<ITMEventRepository>();
            mockTMEventRepository.Setup(x => x.GetAll()).Returns(events);

            Mock<ITMEventAreaService> mockTMEventAreaService = new Mock<ITMEventAreaService>();
            Mock<ITMEventSeatService> mockTMEventSeatService = new Mock<ITMEventSeatService>();

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object,
                 mockTMEventAreaService.Object, mockTMEventSeatService.Object);

            TMEventDto tmeventPre_dto = new TMEventDto
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(-1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            };

            TMEventDto tmeventPast = tmeventService.CreateTMEvent(tmeventPre_dto);

            tmeventPast.Should().BeEquivalentTo(tmeventPre_dto);
        }
    }
}
