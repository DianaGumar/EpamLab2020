using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

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

            Mock<ITMEventRepository> mockTMEventRepository = new Mock<ITMEventRepository>();
            mockTMEventRepository.Setup(x => x.GetAll()).Returns(events);

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object);

            TMEvent tmevent = tmeventService.CreateTMEvent(new TMEvent
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            });

            tmevent.Should().BeEquivalentTo(events[0]);
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

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object);

            TMEvent tmeventPre = new TMEvent
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(-1),
                EndEvent = DateTime.Now.Date,
            };

            TMEvent tmeventPast = tmeventService.CreateTMEvent(tmeventPre);

            tmeventPast.Should().BeEquivalentTo(tmeventPre);
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

            TMEventService tmeventService = new TMEventService(mockTMEventRepository.Object);

            TMEvent tmeventPre = new TMEvent
            {
                Name = "a",
                Description = "d",
                StartEvent = DateTime.Now.Date.AddDays(-1),
                EndEvent = DateTime.Now.Date.AddDays(2),
            };

            TMEvent tmeventPast = tmeventService.CreateTMEvent(tmeventPre);

            tmeventPast.Should().BeEquivalentTo(tmeventPre);
        }
    }
}
