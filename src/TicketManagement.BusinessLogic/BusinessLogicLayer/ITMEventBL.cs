using TicketManagement.DataAccess.Model;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal interface ITMEventBL
    {
        TMEvent CreateTMEvent(TMEvent tmevent);
    }
}
