using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMLayoutRepositoryEF : RepositoryEF<TMLayout>, ITMLayoutRepository
    {
        public TMLayoutRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
