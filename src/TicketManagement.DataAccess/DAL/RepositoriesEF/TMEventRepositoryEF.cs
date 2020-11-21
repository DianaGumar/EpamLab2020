using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
