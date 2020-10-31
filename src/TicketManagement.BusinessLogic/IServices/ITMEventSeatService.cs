using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventSeatService
    {
        TMEventSeatDto CreateTMEventSeat(TMEventSeatDto obj);

        int RemoveTMEventSeat(int id);

        List<TMEventSeatDto> GetAllTMEventSeat();

        int UpdateTMEventSeat(TMEventSeatDto obj);

        TMEventSeatDto GetTMEventSeat(int id);
    }
}
