using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventSeatService
    {
        int RemoveTMEventSeat(int id);

        List<TMEventSeat> GetAllTMEventSeat();

        int UpdateTMEventSeat(TMEventSeat obj);

        TMEventSeat GetTMEventSeat(int id);
    }
}
