using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface IVenueService : IVenueRepository
    {
        Venue CreateVenue(Venue obj);
    }
}
