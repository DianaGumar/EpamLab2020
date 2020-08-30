using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    class EventRepository : IEventRepository
    {
        public void Add(Event obj)
        {
            //call store prosedure

            throw new NotImplementedException();
        }

        public void CreateEvent(Event e, double priseBySeat)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public Event Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venue> GetAllAvailabeleVenues(DateTime startEvent, DateTime endEvent)
        {
            throw new NotImplementedException();
        }

        public void Remove(Event obj)
        {
            throw new NotImplementedException();
        }
    }
}
