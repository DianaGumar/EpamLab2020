using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventService : ITMEventRepository
    {
        TMEvent CreateEvent(TMEvent obj);
    }
}
