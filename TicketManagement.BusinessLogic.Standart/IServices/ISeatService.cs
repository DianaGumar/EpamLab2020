using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface ISeatService
    {
        int RemoveSeat(int id);

        List<SeatDto> GetAllSeat();

        SeatDto GetSeat(int id);

        SeatDto CreateSeat(SeatDto obj);

        int UpdateSeat(SeatDto obj);
    }
}
