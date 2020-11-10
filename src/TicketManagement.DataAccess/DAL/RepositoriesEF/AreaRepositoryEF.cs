using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    // moove validation from servers into repositoryes
    public class AreaRepositoryEF : RepositoryEF<Area>, IAreaRepository
    {
        public AreaRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
