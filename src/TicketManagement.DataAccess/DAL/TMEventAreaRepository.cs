using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class TMEventAreaRepository : Repository<TMEventArea>, ITMEventAreaRepository
    {
        internal TMEventAreaRepository(string conn)
            : base(conn)
        {
        }
    }
}
