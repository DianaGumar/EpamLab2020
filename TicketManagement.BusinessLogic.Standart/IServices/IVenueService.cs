using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface IVenueService
    {
        int RemoveVenue(int id);

        List<VenueDto> GetAllVenue();

        VenueDto GetVenue(int id);

        VenueDto CreateVenue(VenueDto obj);

        int UpdateVenue(VenueDto obj);

        AreaDto CreateArea(AreaDto area, List<SeatDto> seats);

        TMLayoutDto CreateLayout(TMLayoutDto layout, List<AreaDto> areas, List<SeatDto> seats);

        void RemoveLayout(int layoutId);
    }
}
