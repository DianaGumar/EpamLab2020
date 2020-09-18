using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class TMLayoutRepository : Repository<TMLayout>, ITMLayoutRepository
    {
        internal TMLayoutRepository(string conn)
            : base(conn)
        {
        }
    }
}
