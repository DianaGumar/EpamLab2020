using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMLayoutService : ITMLayoutRepository
    {
        TMLayout CreateTMLayout(TMLayout obj);
    }
}
