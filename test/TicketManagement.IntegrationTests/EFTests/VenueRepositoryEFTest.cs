using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.IntegrationTests.EFTests
{
    [TestFixture]
    public class VenueRepositoryEFTest
    {
        [Test]
        public void VenueGetAllTest()
        {
            TMContext context = new TMContext();

            // arange
            VenueRepositoryEF venueRepository = new VenueRepositoryEF(context);

            // act
            List<Venue> venues = venueRepository.GetAll().ToList();

            // assert
            venues.Should().NotBeNull();

            context.Dispose();
        }

        [Test]
        public void VenueCreateTest()
        {
            TMContext context = new TMContext();

            // arange
            IVenueRepository venueRepository = new VenueRepositoryEF(context);

            // act
            Venue venue = venueRepository.Create(
                new Venue
                {
                    Description = "some v desc",
                    Address = "some address",
                    Lenght = 2,
                    Weidth = 2,
                });

            // assert
            venue.Should().BeEquivalentTo(venueRepository.GetById(venue.Id));

            venueRepository.Remove(venue.Id);
            context.Dispose();
        }

        [Test]
        public void VenueUpdateTest()
        {
            TMContext context = new TMContext();

            // arange
            IVenueRepository venueRepository = new VenueRepositoryEF(context);

            // act
            var v = venueRepository.GetById(1);

            v.Description = v.Description + "2";

            venueRepository.Update(v);

            // assert
            Venue vv = venueRepository.GetById(v.Id);

            vv.Should().BeEquivalentTo(v);

            context.Dispose();
        }
    }
}
