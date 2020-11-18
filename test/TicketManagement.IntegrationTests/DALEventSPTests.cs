using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.SqlServer.Dac;
using NUnit.Framework;
using TicketManagement.DataAccess;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.IntegrationTests
{
    public class DALEventSPTests : IDisposable
    {
        // create and delete test database methods for non EF repositoryes
        private const string SCHEMANAME = "TicketManagement_Database_test";
        private const string DACPACPATH =
            "../src/TicketManagement.Database/bin/Debug/TicketManagement.Database.dacpac";

        private static TextWriter _output = new StreamWriter("output.txt", false);

        private readonly string _str =
            ConfigurationManager.ConnectionStrings["DefaultConnection_test"].ConnectionString;

        private readonly TMContext _context = new TMContext("DefaultConnection");
        private bool _isDisposed;

        public static void DropSchema(string schemaName, string connStr)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = $"use master; DROP DATABASE " + schemaName;

            conn.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
        }

        private void CreateDataBase(string schemaName, string connStr, string dacPacFilePath)
        {
            // Class responsible for the deployment. (Connection string supplied by console input for now)
            DacServices dbServices = new DacServices(connStr);

            // Wire up events for Deploy messages and for task progress
            // (For less verbose output, don't subscribe to Message Event (handy for debugging perhaps?)
            dbServices.Message += new EventHandler<DacMessageEventArgs>(DbServices_Message);
            dbServices.ProgressChanged += new EventHandler<DacProgressEventArgs>(DbServices_ProgressChanged);

            // This Snapshot should be created by our build process using MSDeploy
            DacPackage dbPackage = DacPackage.Load(dacPacFilePath);

            DacDeployOptions dbDeployOptions = new DacDeployOptions();

            // Cut out a lot of options here for configuring deployment, but are all part of DacDeployOptions
            dbDeployOptions.SqlCommandVariableValues.Add("debug", "false");

            dbServices.Deploy(dbPackage, schemaName, true, dbDeployOptions);

            _output.Close();
        }

        private void DbServices_Message(object sender, DacMessageEventArgs e)
        {
            _output.WriteLine("DAC Message: {0}", e.Message);
        }

        private void DbServices_ProgressChanged(object sender, DacProgressEventArgs e)
        {
            _output.WriteLine(e.Status + ": " + e.Message);
        }

        // create and delete test database methods for EF repositoryes
        ////[SetUp]
        ////public void Initiaslise()
        ////{
        ////    ////CreateDataBase(SCHEMANAME, _str, DACPACPATH);
        ////    _context.Database.CreateIfNotExists();
        ////}

        ////[TearDown]
        ////public void Cleanup()
        ////{
        ////    ////DropSchema(SCHEMANAME, _str);
        ////    _context.Database.Delete();
        ////    _context.Dispose();
        ////}

        [Test]
        public void CreateEventTest()
        {
            // arange
            IVenueRepository venueRepository = new VenueRepositoryEF(_context);
            ITMLayoutRepository layoutRepository = new TMLayoutRepositoryEF(_context);
            IAreaRepository areaRepository = new AreaRepositoryEF(_context);
            ISeatRepository seatRepository = new SeatRepositoryEF(_context);

            ITMEventRepository eventRepository = new TMEventRepositoryEF(_context);
            ITMEventAreaRepository eventAreaRepository = new TMEventAreaRepositoryEF(_context);
            ITMEventSeatRepository eventSeatRepository = new TMEventSeatRepositoryEF(_context);

            Venue venue = venueRepository.Create(
                new Venue { Description = "some v desc2", Address = "some address2", Lenght = 5, Weidth = 5 });

            TMLayout layout = layoutRepository.Create(new TMLayout { Description = "some desc2", VenueId = venue.Id });

            List<Area> areas = new List<Area>();
            areas.Add(areaRepository.Create(
                new Area { Description = "area12", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id }));
            areas.Add(areaRepository.Create(
                new Area { Description = "area22", CoordX = 0, CoordY = 1, TMLayoutId = layout.Id }));

            List<Seat> seats = new List<Seat>();
            for (int i = 1; i < venue.Lenght + 1; i++)
            {
                areas.ForEach(a => seats.Add(seatRepository.Create(new Seat { Number = i, Row = 1, AreaId = a.Id })));
            }

            // act
            TMEvent tmevent = eventRepository.Create(
                new TMEvent
                {
                    Name = "big event22",
                    Description = "some event desc22",
                    TMLayoutId = layout.Id,
                    StartEvent = DateTime.Now.Date.AddDays(3),
                    EndEvent = DateTime.Now.Date.AddDays(4),
                });

            List<TMEventArea> tmeventareas = eventAreaRepository.GetAll().Where(a => a.TMEventId == tmevent.Id).ToList();
            List<TMEventSeat> tmeventseats = eventSeatRepository.GetAll().
                Where(s => tmeventareas.Any(a => a.Id == s.TMEventAreaId)).ToList();
            TMEvent tmeventFromDB = eventRepository.GetById(tmevent.Id);

            // assert
            tmevent.Should().BeEquivalentTo(tmeventFromDB);

            areas.ToList().Count.Should().Be(tmeventareas.Count);
            tmeventareas.ForEach(ta => ta.Id = 0);
            areas.ForEach(ta => ta.Id = 0);
            tmeventareas.Should().BeEquivalentTo(areas, options => options.ExcludingMissingMembers());

            seats.Count.Should().Be(tmeventseats.Count);
            tmeventseats.ForEach(ta => ta.Id = 0);
            seats.ForEach(ta => ta.Id = 0);
            tmeventseats.Should().BeEquivalentTo(seats, options => options.ExcludingMissingMembers());

            // delete tested data
            seats.ForEach(s => seatRepository.Remove(s.Id));
            areas.ForEach(a => areaRepository.Remove(a.Id));

            eventRepository.Remove(tmevent.Id);
            layoutRepository.Remove(layout.Id);
            venueRepository.Remove(venue.Id);
        }

        [Test]
        public void DeleteEventTest()
        {
            // publish schema by dacpac
            CreateDataBase(SCHEMANAME, _str, DACPACPATH);

            // create tested data
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            ITMEventRepository eventRepository = new TMEventRepository(_str);
            ITMEventAreaRepository eventAreaRepository = new TMEventAreaRepository(_str);
            ITMEventSeatRepository eventSeatRepository = new TMEventSeatRepository(_str);

            Venue venue = venueRepository.Create(
                new Venue { Description = "some v desc2", Address = "some address2", Lenght = 5, Weidth = 5 });

            TMLayout layout = layoutRepository.Create(new TMLayout { Description = "some desc2", VenueId = venue.Id });

            List<Area> areas = new List<Area>();
            areas.Add(areaRepository.Create(
                new Area { Description = "area12", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id }));
            areas.Add(areaRepository.Create(
                new Area { Description = "area22", CoordX = 0, CoordY = 1, TMLayoutId = layout.Id }));

            List<Seat> seats = new List<Seat>();
            for (int i = 1; i < venue.Lenght + 1; i++)
            {
                areas.ForEach(a => seats.Add(seatRepository.Create(new Seat { Number = i, Row = 1, AreaId = a.Id })));
            }

            TMEvent tmevent = eventRepository.Create(
                new TMEvent
                {
                    Name = "big event22",
                    Description = "some event desc22",
                    TMLayoutId = layout.Id,
                    StartEvent = DateTime.Now.Date.AddDays(3),
                    EndEvent = DateTime.Now.Date.AddDays(4),
                });

            // delete tested data
            seats.ForEach(s => seatRepository.Remove(s.Id));
            areas.ForEach(a => areaRepository.Remove(a.Id));

            // act
            eventRepository.Remove(tmevent.Id);
            layoutRepository.Remove(layout.Id);
            venueRepository.Remove(venue.Id);

            List<TMEventArea> tmeventareas = eventAreaRepository.GetAll().Where(a => a.TMEventId == tmevent.Id).ToList();
            List<TMEventSeat> tmeventseats = eventSeatRepository.GetAll().
                Where(s => tmeventareas.Any(a => a.Id == s.TMEventAreaId)).ToList();
            List<TMEvent> tmeventFromBd = eventRepository.GetAll().Where(a => a.Id == tmevent.Id).ToList();

            // assert
            tmeventareas.Count.Should().Be(0);
            tmeventseats.Count.Should().Be(0);
            tmeventFromBd.Count.Should().Be(0);

            // drop schema
            DropSchema(SCHEMANAME, _str);
        }

        [Test]
        public void UpdateEventLocalFieldsTest()
        {
            // publish schema by dacpac
            CreateDataBase(SCHEMANAME, _str, DACPACPATH);

            // create tested data
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            ITMEventRepository eventRepository = new TMEventRepository(_str);

            Venue venue = venueRepository.Create(
                new Venue { Description = "some v desc2", Address = "some address2", Lenght = 5, Weidth = 5 });

            TMLayout layout = layoutRepository.Create(new TMLayout { Description = "some desc2", VenueId = venue.Id });

            List<Area> areas = new List<Area>();
            areas.Add(areaRepository.Create(
                new Area { Description = "area12", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id }));
            areas.Add(areaRepository.Create(
                new Area { Description = "area22", CoordX = 0, CoordY = 1, TMLayoutId = layout.Id }));

            List<Seat> seats = new List<Seat>();
            for (int i = 1; i < venue.Lenght + 1; i++)
            {
                areas.ForEach(a => seats.Add(seatRepository.Create(new Seat { Number = i, Row = 1, AreaId = a.Id })));
            }

            TMEvent tmevent = eventRepository.Create(
                new TMEvent
                {
                    Name = "big event22",
                    Description = "some event desc22",
                    TMLayoutId = layout.Id,
                    StartEvent = DateTime.Now.Date.AddDays(3),
                    EndEvent = DateTime.Now.Date.AddDays(4),
                });

            // was some second create
            // act
            tmevent.Description = "new desc";
            tmevent.StartEvent = DateTime.Now.Date.AddDays(5);
            tmevent.EndEvent = DateTime.Now.Date.AddDays(6);

            eventRepository.Update(tmevent);

            TMEvent tmeventFromDB = eventRepository.GetById(tmevent.Id);

            // delete tested data
            seats.ForEach(s => seatRepository.Remove(s.Id));
            areas.ForEach(a => areaRepository.Remove(a.Id));

            eventRepository.Remove(tmevent.Id);
            layoutRepository.Remove(layout.Id);
            venueRepository.Remove(venue.Id);

            // assert
            tmevent.Should().BeEquivalentTo(tmeventFromDB);

            // drop schema
            DropSchema(SCHEMANAME, _str);
        }

        [Test]
        public void UpdateEventLayoutTest()
        {
            // publish schema by dacpac
            CreateDataBase(SCHEMANAME, _str, DACPACPATH);

            // create tested data
            // arange
            IVenueRepository venueRepository = new VenueRepository(_str);
            ITMLayoutRepository layoutRepository = new TMLayoutRepository(_str);
            IAreaRepository areaRepository = new AreaRepository(_str);
            ISeatRepository seatRepository = new SeatRepository(_str);

            ITMEventRepository eventRepository = new TMEventRepository(_str);
            ITMEventAreaRepository eventAreaRepository = new TMEventAreaRepository(_str);
            ITMEventSeatRepository eventSeatRepository = new TMEventSeatRepository(_str);

            Venue venue = venueRepository.Create(
                new Venue { Description = "some v desc2", Address = "some address2", Lenght = 5, Weidth = 5 });

            TMLayout layout = layoutRepository.Create(new TMLayout { Description = "some desc2", VenueId = venue.Id });
            TMLayout layout2 = layoutRepository.Create(new TMLayout { Description = "some desc22", VenueId = venue.Id });

            List<Area> areas = new List<Area>();
            areas.Add(areaRepository.Create(
                new Area { Description = "area12", CoordX = 0, CoordY = 0, TMLayoutId = layout.Id }));
            areas.Add(areaRepository.Create(
                new Area { Description = "area22", CoordX = 0, CoordY = 1, TMLayoutId = layout.Id }));

            List<Area> areas2 = new List<Area>();
            areas2.Add(areaRepository.Create(
                new Area { Description = "area122", CoordX = 1, CoordY = 0, TMLayoutId = layout2.Id }));
            areas2.Add(areaRepository.Create(
                new Area { Description = "area222", CoordX = 1, CoordY = 1, TMLayoutId = layout2.Id }));

            List<Seat> seats = new List<Seat>();
            for (int i = 1; i < venue.Lenght + 1; i++)
            {
                areas.ForEach(a => seats.Add(seatRepository.Create(new Seat { Number = i, Row = 1, AreaId = a.Id })));
            }

            List<Seat> seats2 = new List<Seat>();
            for (int i = 1; i < venue.Lenght + 1; i++)
            {
                areas2.ForEach(a => seats2.Add(seatRepository.Create(new Seat { Number = i, Row = 1, AreaId = a.Id })));
            }

            TMEvent tmevent = eventRepository.Create(
                new TMEvent
                {
                    Name = "big event22",
                    Description = "some event desc22",
                    TMLayoutId = layout.Id,
                    StartEvent = new DateTime(2020, 9, 25),
                    EndEvent = new DateTime(2020, 9, 26),
                });

            // act
            tmevent.Description = "new desc";
            tmevent.TMLayoutId = layout2.Id;

            eventRepository.Update(tmevent);

            List<TMEventArea> tmeventareas = eventAreaRepository.GetAll().Where(a => a.TMEventId == tmevent.Id).ToList();
            List<TMEventSeat> tmeventseats = eventSeatRepository.GetAll().
                Where(s => tmeventareas.Any(a => a.Id == s.TMEventAreaId)).ToList();
            TMEvent tmeventFromDB = eventRepository.GetById(tmevent.Id);

            // delete tested data
            seats.ForEach(s => seatRepository.Remove(s.Id));
            seats2.ForEach(s => seatRepository.Remove(s.Id));
            areas.ForEach(a => areaRepository.Remove(a.Id));
            areas2.ForEach(a => areaRepository.Remove(a.Id));

            eventRepository.Remove(tmevent.Id);
            layoutRepository.Remove(layout.Id);
            layoutRepository.Remove(layout2.Id);
            venueRepository.Remove(venue.Id);

            // assert
            tmevent.Should().BeEquivalentTo(tmeventFromDB);

            areas2.ToList().Count.Should().Be(tmeventareas.Count);
            tmeventareas.ForEach(ta => ta.Id = 0);
            areas2.ForEach(ta => ta.Id = 0);
            tmeventareas.Should().BeEquivalentTo(areas2, options => options.ExcludingMissingMembers());

            seats2.Count.Should().Be(tmeventseats.Count);
            tmeventseats.ForEach(ta => ta.Id = 0);
            seats2.ForEach(ta => ta.Id = 0);
            tmeventseats.Should().BeEquivalentTo(seats2, options => options.ExcludingMissingMembers());

            // drop schema
            DropSchema(SCHEMANAME, _str);
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
