using System.Collections.Generic;
using TicketManagement.Domain;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventAreasBL
    {
        void SetTMEventAreaPrice(int areaId, decimal price);

        List<TMEventAreaModels> GetAllTMEventAreas();

        TMEventAreaModels GetTMEventArea(int id);
    }
}
