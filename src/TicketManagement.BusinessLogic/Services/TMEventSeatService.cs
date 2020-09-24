using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventSeatService : TMEventSeatRepository, ITMEventSeatService
    {
        internal TMEventSeatService(string connectString)
            : base(connectString)
        {
        }

        public void SetState(int tmeventSeatId, int state)
        {
            TMEventSeat tmeventSeat = GetById(tmeventSeatId);
            tmeventSeat.State = state;
            Update(tmeventSeat);
        }
    }
}
