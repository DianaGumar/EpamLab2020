using System.Collections.Generic;
using TicketManagement.Domain;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventAreasBL
    {
        int SetTMEventAreaPrice(int areaId, decimal price);

        List<TMEventAreaModels> GetAllTMEventAreas();

        TMEventAreaModels GetTMEventArea(int id);

        List<TMEventSeatModels> GetTMEventSeats(int tmeventAreaId);

        int SetTMEventSeatState(int tmeventSeatId, SeatState state);
    }
}
