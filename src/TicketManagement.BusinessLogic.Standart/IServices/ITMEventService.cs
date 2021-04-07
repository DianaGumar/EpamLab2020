using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface ITMEventService
    {
        TMEventStatus RemoveTMEvent(int id);

        TMEventDto GetTMEvent(int id);

        List<TMEventDto> GetAllTMEvent();

        List<TMEventDto> GetAllRelevantTMEvent();

        TMEventStatus CreateTMEvent(TMEventDto obj);

        TMEventStatus UpdateTMEvent(int id, TMEventDto obj);

        List<TMEventSeatDto> GetTMEventSeatByEvent(int eventId);

        List<TMEventAreaDto> GetTMEventAreaByEvent(int eventId);
    }
}
