using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventService
    {
        int RemoveTMEvent(int id);

        TMEvent GetTMEvent(int id);

        List<TMEvent> GetAllTMEvent();

        TMEvent CreateTMEvent(TMEvent obj);

        int UpdateTMEvent(TMEvent obj);
    }
}
