using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventSeatRepositoryEF : RepositoryEF<TMEventSeat>, ITMEventSeatRepository
    {
        public TMEventSeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
