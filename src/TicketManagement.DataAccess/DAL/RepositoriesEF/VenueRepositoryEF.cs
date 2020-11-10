using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class VenueRepositoryEF : RepositoryEF<Venue>, IVenueRepository
    {
        public VenueRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
