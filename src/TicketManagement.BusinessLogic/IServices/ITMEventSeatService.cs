using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventSeatService
    {
        int RemoveTMEventSeat(int id);

        List<TMEventSeat> GetAllTMEventSeat();

        void SetState(int tmeventSeatId, int state);
    }
}
