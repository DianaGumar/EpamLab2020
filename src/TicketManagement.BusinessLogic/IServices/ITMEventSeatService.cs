using TicketManagement.DataAccess.DAL;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventSeatService : ITMEventSeatRepository
    {
        void SetState(int tmeventSeatId, int state);
    }
}
