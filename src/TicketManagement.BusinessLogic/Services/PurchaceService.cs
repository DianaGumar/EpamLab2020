using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public interface IPurchaceService
    {
        List<PurchaseHistoryDto> GetPurchaseHistory(string userId);
    }

    public class PurchaceService : IPurchaceService
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;
        private readonly ITMEventService _tmeventService;

        public PurchaceService(IPurchaseHistoryRepository purchaseHistoryRepository,
            ITMEventAreaService tmeventAreaService, ITMEventService tmeventService,
            ITMEventSeatService tmeventSeatService)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _tmeventAreaService = tmeventAreaService;
            _tmeventService = tmeventService;
            _tmeventSeatService = tmeventSeatService;
        }

        public List<PurchaseHistoryDto> GetPurchaseHistory(string userId)
        {
            List<PurchaseHistory> ph =
                _purchaseHistoryRepository.GetAll().Where(p => p.UserId == userId).ToList();

            var phdto = new List<PurchaseHistoryDto>();

            foreach (var item in ph)
            {
                TMEventSeatDto seat = _tmeventSeatService.GetTMEventSeat(item.TMEventSeatId);
                TMEventAreaDto area = _tmeventAreaService.GetTMEventArea(seat.TMEventAreaId);

                phdto.Add(new PurchaseHistoryDto
                {
                    SeatObj = seat,
                    BookingDate = item.BookingDate,
                    AreaPrice = area.Price,
                    TMEventObj = _tmeventService.GetTMEvent(area.TMEventId),
                });
            }

            return phdto;
        }
    }
}
