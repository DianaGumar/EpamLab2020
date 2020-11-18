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
    public class TMEventRepositoryEFTest : IDisposable
    {
        private readonly TMContext _context = new TMContext("DefaultConnection_ef_tests");
        private bool _isDisposed;

        [SetUp]
        public void Initiaslise()
        {
            _context.Database.CreateIfNotExists();
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.Delete();
            _context.Dispose();
        }

        [Test]
        public void TMEventGetAll()
        {
            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(_context);

            // act
            List<TMEvent> objs = objRepository.GetAll().ToList();

            // assert
            objs.Should().NotBeNull();
        }

        [Test]
        public void TMEventCreate()
        {
            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(_context);

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
        }

        [Test]
        public void TMEventUpdate()
        {
            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(_context);
            TMEvent e = objRepository.GetAll().Last();

            // act
            e.Name = "new updated name";
            objRepository.Update(e);

            // assert
            TMEvent ee = objRepository.GetAll().Last();
            ee.Should().BeEquivalentTo(e);
        }

        [Test]
        public void TMEventDelete()
        {
            // arange
            ITMEventRepository objRepository = new TMEventRepositoryEF(_context);

            int id = objRepository.GetAll().Last().Id;

            // act
            objRepository.Remove(id);
            TMEvent e = objRepository.GetById(id);

            // assert
            e.Should().BeNull();
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // free managed resources
                _context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
