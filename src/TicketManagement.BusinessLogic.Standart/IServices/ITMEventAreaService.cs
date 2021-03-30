using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface ITMEventAreaService
    {
        List<TMEventAreaDto> GetAllTMEventArea();

        TMEventAreaDto GetTMEventArea(int id);

        int UpdateTMEventArea(TMEventAreaDto obj);

        int RemoveTMEventArea(int id);

        TMEventAreaDto CreateTMEventArea(TMEventAreaDto obj);

        List<TMEventSeatDto> GetTMEventSeatsByArea(int tmeventAreaId);

        List<TMEventSeatDto> GetTMEventSeatsByIds(params int[] seatsId);

        int SetTMEventSeatState(int tmeventSeatId, SeatState state);

        int SetTMEventAreaPrice(int areaId, decimal price);

        decimal GetTMEventAreaPrice(int areaId);
    }
}
