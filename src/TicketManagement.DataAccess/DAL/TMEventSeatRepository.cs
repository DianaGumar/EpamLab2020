using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class TMEventSeatRepository : Repository<TMEventSeat>, ITMEventSeatRepository
    {
        internal TMEventSeatRepository(string conn)
            : base(conn)
        {
        }
    }
}
