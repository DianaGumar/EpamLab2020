using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class SeatRepositoryEF : RepositoryEF<Seat>, ISeatRepository
    {
        public SeatRepositoryEF(DbContext conn)
            : base(conn)
        {
        }
    }
}
