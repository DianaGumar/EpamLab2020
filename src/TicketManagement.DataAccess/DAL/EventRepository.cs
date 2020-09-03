using System;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public class EventRepository : Repository<PublicEvent>, IEventRepository
    {
        public EventRepository(string conn)
            : base(conn)
        {
        }

        public void CreateEvent(PublicEvent e, double priseBySeat)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(PublicEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
