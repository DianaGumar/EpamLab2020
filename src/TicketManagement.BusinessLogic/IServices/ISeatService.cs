using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ISeatService : ISeatRepository
    {
        Seat CreateSeat(Seat obj);
    }
}
