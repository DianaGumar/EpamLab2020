using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface IVenueService
    {
        int RemoveVenue(int id);

        List<Venue> GetAllVenue();

        Venue CreateVenue(Venue obj);
    }
}
