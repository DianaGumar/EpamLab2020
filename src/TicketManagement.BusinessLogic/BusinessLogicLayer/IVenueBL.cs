using System.Collections.Generic;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface IVenueBL
    {
        AreaDto CreateArea(AreaDto area, List<Seat> seats);

        TMLayoutDto CreateLayout(TMLayoutDto layout);

        void RemoveLayout(int layoutId);

        VenueDto CreateVenue(VenueDto obj);

        List<VenueDto> GetAllVenues();

        List<TMLayoutDto> GetAllLayouts();
    }
}
