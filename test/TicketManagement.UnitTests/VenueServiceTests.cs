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
    public class VenueServiceTests
    {
        [Test]
        public void CreateVenueTestSameObject()
        {
            List<Venue> venues = new List<Venue>();
            venues.Add(new Venue { Id = 1, Address = "a", Description = "d" });
            venues.Add(new Venue { Id = 2, Address = "a2", Description = "d2" });
            venues.Add(new Venue { Id = 3, Address = "a3", Description = "d3" });

            List<VenueDto> venues_dto = new List<VenueDto>();
            venues_dto.Add(new VenueDto { Id = 1, Address = "a", Description = "d" });
            venues_dto.Add(new VenueDto { Id = 2, Address = "a2", Description = "d2" });
            venues_dto.Add(new VenueDto { Id = 3, Address = "a3", Description = "d3" });

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            mockVenueRepository.Setup(x => x.GetAll()).Returns(venues);
            Mock<ISeatService> mockSeatService = new Mock<ISeatService>();
            Mock<ITMLayoutService> mockTMLayoutService = new Mock<ITMLayoutService>();
            Mock<IAreaService> mockAreaService = new Mock<IAreaService>();

            VenueService venueService = new VenueService(mockVenueRepository.Object,
                mockTMLayoutService.Object, mockAreaService.Object, mockSeatService.Object);

            VenueDto venue = venueService.CreateVenue(new VenueDto { Address = "a", Description = "d" });

            venue.Should().BeEquivalentTo(venues_dto[0]);
        }

        [Test]
        public void CreateVenueTest()
        {
            List<Venue> venues = new List<Venue>();
            venues.Add(new Venue { Id = 1, Address = "a", Description = "d" });
            venues.Add(new Venue { Id = 2, Address = "a2", Description = "d2" });
            venues.Add(new Venue { Id = 3, Address = "a3", Description = "d3" });

            VenueDto venueCreate_service = new VenueDto { Address = "a4", Description = "d4" };
            Venue venuePost = new Venue { Id = 4, Address = "a4", Description = "d4" };
            VenueDto venuePost_service = new VenueDto { Id = 4, Address = "a4", Description = "d4" };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            mockVenueRepository.Setup(x => x.GetAll()).Returns(venues);
            mockVenueRepository.Setup(x => x.Create(It.IsAny<Venue>())).Returns(venuePost);

            Mock<ISeatService> mockSeatService = new Mock<ISeatService>();
            mockSeatService.Setup(x => x.CreateSeat(It.IsAny<SeatDto>()))
                .Returns(new SeatDto { Id = 1 });

            Mock<ITMLayoutService> mockTMLayoutService = new Mock<ITMLayoutService>();
            mockTMLayoutService.Setup(x => x.CreateTMLayout(It.IsAny<TMLayoutDto>()))
                .Returns(new TMLayoutDto { Id = 1 });

            Mock<IAreaService> mockAreaService = new Mock<IAreaService>();
            mockAreaService.Setup(x => x.CreateArea(It.IsAny<AreaDto>()))
                .Returns(new AreaDto { Id = 1 });

            VenueService venueService = new VenueService(mockVenueRepository.Object,
                mockTMLayoutService.Object, mockAreaService.Object, mockSeatService.Object);

            VenueDto venue = venueService.CreateVenue(venueCreate_service);

            venue.Should().BeEquivalentTo(venuePost_service);
        }

        [Test]
        public void GetAllVenue()
        {
            Mock<ISeatService> mockSeatService = new Mock<ISeatService>();
            Mock<ITMLayoutService> mockTMLayoutService = new Mock<ITMLayoutService>();
            Mock<IAreaService> mockAreaService = new Mock<IAreaService>();
            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            VenueService venueService = new VenueService(mockVenueRepository.Object,
                mockTMLayoutService.Object, mockAreaService.Object, mockSeatService.Object);

            List<VenueDto> venues = venueService.GetAllVenue();

            venues.Should().NotBeNull();
        }
    }
}
