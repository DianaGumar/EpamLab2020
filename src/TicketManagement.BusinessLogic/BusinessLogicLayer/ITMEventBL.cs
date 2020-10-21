using System.Collections.Generic;
using TicketManagement.Domain;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventBL
    {
        List<TMEventModels> GetAllTMEvent();

        TMEventModels GetTMEvent(int id);

        TMEventModels CreateTMEvent(TMEventModels tmevent);
    }
}
