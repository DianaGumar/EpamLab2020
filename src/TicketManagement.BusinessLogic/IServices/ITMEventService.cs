using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventService
    {
        int RemoveTMEvent(int id);

        TMEventDto GetTMEvent(int id);

        List<TMEventDto> GetAllTMEvent();

        TMEventDto CreateTMEvent(TMEventDto obj);

        int UpdateTMEvent(TMEventDto obj);
    }
}
