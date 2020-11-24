using System;
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

        int BuyTicket(string userId, int tmeventSeatId);

        int BuyTicket(string userId, params int[] tmeventSeatId);

        List<TMEventSeatDto> GetAllTMEventSeatsByArea(int tmeventAreaId);
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

        // make method by one transaction
        public int BuyTicket(string userId, int tmeventSeatId)
        {
            int i = _tmeventAreaService.SetTMEventSeatState(tmeventSeatId, SeatState.Busy);

            PurchaseHistory ph = _purchaseHistoryRepository.Create(new PurchaseHistory
            {
                UserId = userId,
                TMEventSeatId = tmeventSeatId,
                BookingDate = DateTime.Now,
            });

            return ph.Id > 0 && i > 0 ? 1 : 0;
        }

        public int BuyTicket(string userId, params int[] tmeventSeatId)
        {
            int result = 0;

            foreach (var item in tmeventSeatId)
            {
                result += BuyTicket(userId, item);
            }

            return result == tmeventSeatId.Length ? 1 : 0;
        }

        public List<TMEventSeatDto> GetAllTMEventSeatsByArea(int tmeventAreaId)
        {
            return _tmeventAreaService.GetTMEventSeatsByArea(tmeventAreaId);
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
