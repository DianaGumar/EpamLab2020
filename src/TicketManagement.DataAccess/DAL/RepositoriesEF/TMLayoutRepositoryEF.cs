using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMLayoutRepositoryEF : RepositoryEF<TMLayout>, ITMLayoutRepository
    {
        public TMLayoutRepositoryEF(DbContext conn)
            : base(conn)
        {
        }
    }
}
