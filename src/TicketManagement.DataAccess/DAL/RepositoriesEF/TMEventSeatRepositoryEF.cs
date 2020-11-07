using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventSeatRepositoryEF : RepositoryEF<TMEventSeat>, ITMEventSeatRepository
    {
        public TMEventSeatRepositoryEF(DbContext conn)
            : base(conn)
        {
        }
    }
}
