using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventBL
    {
        List<TMEventDto> GetAllTMEvent();

        TMEventDto GetTMEvent(int id);

        TMEventDto CreateTMEvent(TMEventDto tmevent);

        int UpdateTMEvent(TMEventDto tmevent);

        int DeleteTMEvent(int id);
    }
}
