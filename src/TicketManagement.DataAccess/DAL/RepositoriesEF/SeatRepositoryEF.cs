using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class SeatRepositoryEF : RepositoryEF<Seat>, ISeatRepository
    {
        public SeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
