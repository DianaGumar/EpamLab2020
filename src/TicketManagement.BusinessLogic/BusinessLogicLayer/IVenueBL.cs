using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface IVenueBL
    {
        Area CreateArea(Area area, ref List<Seat> seats);

        TMLayout CreateLayout(TMLayout layout);

        void RemoveLayout(int layoutId);

        Venue CreateVenue(Venue obj);
    }
}
