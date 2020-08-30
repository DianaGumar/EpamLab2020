using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public interface IEventRepository : IRepository<Event>
    {
        void CreateEvent(Event e, double priseBySeat);
        void DeleteEvent(Event e);


        //should be in logic part
        //thats need to know to create event
        IEnumerable<Venue> GetAllAvailabeleVenues(DateTime startEvent, DateTime endEvent);
        IEnumerable<Venue> GetAllVenues();

        IEnumerable<Area> GetAllLayoutByVenue(int id);


        //void SetPriseBySeat(double prise);  ?


        //void UpdateEvent();    IUnitOfWork ?
    }
}
