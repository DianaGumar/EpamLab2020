using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventAreaRepositoryEF : RepositoryEF<TMEventArea>, ITMEventAreaRepository
    {
        public TMEventAreaRepositoryEF(DbContext conn)
            : base(conn)
        {
        }
    }
}
