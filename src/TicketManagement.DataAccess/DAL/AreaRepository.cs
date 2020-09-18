using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class AreaRepository : Repository<Area>, IAreaRepository
    {
        internal AreaRepository(string conn)
            : base(conn)
        {
        }
    }
}
