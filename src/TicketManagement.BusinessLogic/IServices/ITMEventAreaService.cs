using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventAreaService
    {
        List<TMEventAreaDto> GetAllTMEventArea();

        TMEventAreaDto GetTMEventArea(int id);

        int UpdateTMEventArea(TMEventAreaDto obj);

        int RemoveTMEventArea(int id);

        TMEventAreaDto CreateTMEventArea(TMEventAreaDto obj);
    }
}
