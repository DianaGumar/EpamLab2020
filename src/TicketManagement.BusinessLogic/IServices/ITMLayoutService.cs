using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMLayoutService
    {
        int RemoveTMLayout(int id);

        List<TMLayoutDto> GetAllTMLayout();

        TMLayoutDto GetTMLayout(int id);

        TMLayoutDto CreateTMLayout(TMLayoutDto obj);

        int UpdateTMLayout(TMLayoutDto obj);
    }
}
