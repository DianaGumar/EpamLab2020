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

    public class PurchaseHistoryRepositoryEF : RepositoryEF<PurchaseHistory>, IPurchaseHistoryRepository
    {
        public PurchaseHistoryRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class SeatRepositoryEF : RepositoryEF<Seat>, ISeatRepository
    {
        public SeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMEventAreaRepositoryEF : RepositoryEF<TMEventArea>, ITMEventAreaRepository
    {
        public TMEventAreaRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMEventSeatRepositoryEF : RepositoryEF<TMEventSeat>, ITMEventSeatRepository
    {
        public TMEventSeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMLayoutRepositoryEF : RepositoryEF<TMLayout>, ITMLayoutRepository
    {
        public TMLayoutRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMUserRepositoryEF : RepositoryEF<TMUser>, ITMUserRepository
    {
        public TMUserRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class VenueRepositoryEF : RepositoryEF<Venue>, IVenueRepository
    {
        public VenueRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
