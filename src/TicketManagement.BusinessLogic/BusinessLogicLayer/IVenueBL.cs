using System.Collections.Generic;
using TicketManagement.DataAccess.Model;
using TicketManagement.Domain;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface IVenueBL
    {
        AreaModels CreateArea(AreaModels area, List<Seat> seats);

        TMLayoutModels CreateLayout(TMLayoutModels layout);

        void RemoveLayout(int layoutId);

        VenueModels CreateVenue(VenueModels obj);

        List<VenueModels> GetAllVenues();

        List<TMLayoutModels> GetAllLayouts();
    }
}
