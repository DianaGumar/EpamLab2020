using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ISeatService
    {
        int RemoveSeat(int id);

        List<Seat> GetAllSeat();

        Seat CreateSeat(Seat obj);
    }
}
