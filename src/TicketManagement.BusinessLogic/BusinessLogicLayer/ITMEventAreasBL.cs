using System.Collections.Generic;
using TicketManagement.Domain;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventAreasBL
    {
        int SetTMEventAreaPrice(int areaId, decimal price);

        List<TMEventAreaDto> GetAllTMEventAreas();

        TMEventAreaDto GetTMEventArea(int id);

        List<TMEventAreaDto> GetTMEventSeats(int tmeventAreaId);

        int SetTMEventSeatState(int tmeventSeatId, SeatState state);
    }
}
