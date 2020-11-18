using System;
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
    public class TMEventRepositoryEFTest
    {
        [Test]
        public void TMEventGetAllTest()
        {
            TMContext context = new TMContext();

            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(context);

            // act
            List<TMEvent> objs = objRepository.GetAll().ToList();

            // assert
            objs.Should().NotBeNull();

            context.Dispose();
        }

        [Test]
        public void TMEventCreateTest()
        {
            TMContext context = new TMContext();

            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(context);

            // act
            TMEvent tmevent = objRepository.Create(
                new TMEvent
                {
                    Name = "big event33",
                    Description = "some event desc33",
                    TMLayoutId = 1,
                    StartEvent = new DateTime(2020, 11, 25),
                    EndEvent = new DateTime(2020, 11, 26),
                    Img = "https://upload.wikimedia.org/wikipedia/en/6/60/No_Picture.jpg",
                });

            // assert
            tmevent.Should().BeEquivalentTo(objRepository.GetById(tmevent.Id));

            objRepository.Remove(tmevent.Id);
            context.Dispose();
        }

        [Test]
        public void TMEventUpdateTest()
        {
            TMContext context = new TMContext();

            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(context);
            TMEvent e = objRepository.GetAll().Last();

            // act
            e.Name = "new updated name";
            objRepository.Update(e);

            // assert
            TMEvent ee = objRepository.GetAll().Last();
            ee.Should().BeEquivalentTo(e);

            context.Dispose();
        }

        [Test]
        public void TMEventDeleteTest()
        {
            TMContext context = new TMContext();

            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(context);

            int id = objRepository.GetAll().Last().Id;

            // act
            objRepository.Remove(id);
            TMEvent e = objRepository.GetById(id);

            // assert
            e.Should().BeNull();

            context.Dispose();
        }
    }
}
