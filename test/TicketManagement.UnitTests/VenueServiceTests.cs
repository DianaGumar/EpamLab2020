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

            VenueService venueService = new VenueService(mockVenueRepository.Object);

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

            Venue venueCreate = new Venue { Address = "a4", Description = "d4" };
            VenueDto venueCreate_service = new VenueDto { Address = "a4", Description = "d4" };
            Venue venuePost = new Venue { Id = 4, Address = "a4", Description = "d4" };
            VenueDto venuePost_service = new VenueDto { Id = 4, Address = "a4", Description = "d4" };

            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            mockVenueRepository.Setup(x => x.GetAll()).Returns(venues);
            mockVenueRepository.Setup(x => x.Create(venueCreate)).Returns(venuePost);

            VenueService venueService = new VenueService(mockVenueRepository.Object);

            VenueDto venue = venueService.CreateVenue(venueCreate_service);

            venue.Should().BeEquivalentTo(venuePost_service);
        }

        [Test]
        public void GetAllVenue()
        {
            Mock<IVenueRepository> mockVenueRepository = new Mock<IVenueRepository>();
            VenueService venueService = new VenueService(mockVenueRepository.Object);

            List<VenueDto> venues = venueService.GetAllVenue();

            venues.Should().NotBeNull();
        }
    }
}
