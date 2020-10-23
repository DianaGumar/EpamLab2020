using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventService
    {
        int RemoveTMEvent(int id);

        List<TMEvent> GetAllTMEvent();

        TMEvent CreateTMEvent(TMEvent obj);
    }
}
