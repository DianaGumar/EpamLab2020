using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventAreaRepositoryEF : RepositoryEF<TMEventArea>, ITMEventAreaRepository
    {
        public TMEventAreaRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
