using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.IntegrationTests
{
    public class DALEventSPTests
    {
        private readonly string _str = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        // Same test data are located
        // TicketManagement.Database/Post/Script.TestEventSPData.sql
        [Test]
        public void CreateEventTest()
        {
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

            // act
            TMEvent tmevent = eventRepository.Create(
                new TMEvent
                {
                    Name = "big event22",
                    Description = "some event desc22",
                    TMLayoutId = layout.Id,
                    StartEvent = new DateTime(2020, 9, 25),
                    EndEvent = new DateTime(2020, 9, 26),
                });

            List<TMEventArea> tmeventareas = eventAreaRepository.GetAll().Where(a => a.TMEventId == tmevent.Id).ToList();
            List<TMEventSeat> tmeventseats = eventSeatRepository.GetAll().
                Where(s => tmeventareas.Any(a => a.Id == s.TMEventAreaId)).ToList();
            TMEvent tmeventFromDB = eventRepository.GetById(tmevent.Id);

            // delete tested data
            seats.ForEach(s => seatRepository.Remove(s.Id));
            areas.ForEach(a => areaRepository.Remove(a.Id));

            eventRepository.Remove(tmevent.Id);
            layoutRepository.Remove(layout.Id);
            venueRepository.Remove(venue.Id);

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
        }

        [Test]
        public void DeleteEventTest()
        {
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
                    StartEvent = new DateTime(2020, 9, 25),
                    EndEvent = new DateTime(2020, 9, 26),
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
        }

        [Test]
        public void UpdateEventLocalFieldsTest()
        {
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
                    StartEvent = new DateTime(2020, 9, 25),
                    EndEvent = new DateTime(2020, 9, 26),
                });

            // act
            tmevent.Description = "new desc";
            tmevent.StartEvent = new DateTime(2020, 10, 25);
            tmevent.EndEvent = new DateTime(2020, 10, 26);

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
        }

        [Test]
        public void UpdateEventLayoutTest()
        {
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
        }
    }
}
