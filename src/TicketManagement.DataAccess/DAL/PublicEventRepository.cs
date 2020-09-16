using System;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public class PublicEventRepository : Repository<PublicEvent>, IPublicEventRepository
    {
        public PublicEventRepository(string conn)
            : base(conn)
        {
        }

        public new int Create(PublicEvent obj)
        {
            throw new NotImplementedException();
        }

        public void CreateEvent(PublicEvent obj, double priseBySeat)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(PublicEvent obj)
        {
            throw new NotImplementedException();
        }
    }
}
