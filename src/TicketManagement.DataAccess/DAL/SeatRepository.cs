using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class SeatRepository : Repository<Seat>, ISeatRepository
    {
        internal SeatRepository(string conn)
            : base(conn)
        {
        }
    }
}
