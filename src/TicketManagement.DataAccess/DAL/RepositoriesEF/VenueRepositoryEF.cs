using System.Data.Entity;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class VenueRepositoryEF : RepositoryEF<Venue>, IVenueRepository
    {
        public VenueRepositoryEF(DbContext conn)
            : base(conn)
        {
        }
    }
}
