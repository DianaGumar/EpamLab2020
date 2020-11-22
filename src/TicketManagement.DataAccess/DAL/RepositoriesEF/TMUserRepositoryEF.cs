using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMUserRepositoryEF : RepositoryEF<TMUser>, ITMUserRepository
    {
        public TMUserRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
