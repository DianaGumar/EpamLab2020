using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public enum PurchaseStatus
    {
        NotEnothMoney = 1,
        Wait = 2,
        PurchaseSucsess = 3,
        NotRelevantSeats = 4,
        ReturnTicketSucsess = 5,
        ReturnTicketFailWithPastEvent = 6,
    }

    public class PurchaceService : IPurchaceService
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;
        private readonly ITMEventService _tmeventService;

        private readonly IUserService _userService;

        public PurchaceService(IPurchaseHistoryRepository purchaseHistoryRepository,
            ITMEventAreaService tmeventAreaService, ITMEventService tmeventService,
            ITMEventSeatService tmeventSeatService, IUserService userService)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _tmeventAreaService = tmeventAreaService;
            _tmeventService = tmeventService;
            _tmeventSeatService = tmeventSeatService;
            _userService = userService;
        }

        // make method by one transaction
        public PurchaseStatus BuyTicket(string userId, int tmeventSeatId)
        {
            int i = _tmeventAreaService.SetTMEventSeatState(tmeventSeatId, SeatState.Busy);

            PurchaseHistory ph = _purchaseHistoryRepository.Create(new PurchaseHistory
            {
                UserId = userId,
                TMEventSeatId = tmeventSeatId,
                BookingDate = DateTime.Now,
            });

            return ph.Id > 0 && i > 0 ? PurchaseStatus.PurchaseSucsess : PurchaseStatus.Wait;
        }

        public PurchaseStatus BuyTicket(string userId, params int[] tmeventSeatId)
        {
            PurchaseStatus result = PurchaseStatus.PurchaseSucsess;

            // find price
            decimal price = 0;
            List<TMEventSeatDto> seats = _tmeventAreaService.GetTMEventSeatsByIds(tmeventSeatId)
                .Where(s => s.State == SeatState.Free).ToList();

            if (seats.Count == 0)
            {
                return PurchaseStatus.NotRelevantSeats;
            }

            tmeventSeatId = seats.Select(s => s.Id).ToArray();

            List<int> areasId = seats.Select(s => s.TMEventAreaId).Distinct().ToList();

            areasId.ForEach(a => price += _tmeventAreaService.GetTMEventAreaPrice(a));

            // check sufficiency user balance
            if (_userService.IsBaslanceEnough(userId, price))
            {
                // to do make one transaction
                foreach (var item in tmeventSeatId)
                {
                    result = BuyTicket(userId, item);
                    if (result == PurchaseStatus.Wait)
                    {
                        break;
                    }
                }

                if (result == PurchaseStatus.Wait)
                {
                    foreach (var item in tmeventSeatId)
                    {
                        _tmeventAreaService.SetTMEventSeatState(item, SeatState.Free);
                    }
                }
                else
                {
                    // pay money
                    _userService.MakePurchase(userId, price);
                }
            }
            else
            {
                return PurchaseStatus.NotEnothMoney;
            }

            return result;
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

        public PurchaseStatus ReturnTicket(string userId, int tmeventSeatId)
        {
            TMEventSeatDto seat = _tmeventSeatService.GetTMEventSeat(tmeventSeatId);
            TMEventAreaDto area = _tmeventAreaService.GetTMEventArea(seat.TMEventAreaId);
            var tmeventObj = _tmeventService.GetTMEvent(area.TMEventId);

            if (tmeventObj.StartEvent > DateTime.Now)
            {
                _tmeventAreaService.SetTMEventSeatState(tmeventSeatId, SeatState.Free);

                var ph = _purchaseHistoryRepository.GetAll()
                    .Where(p => p.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault(p => p.TMEventSeatId == tmeventSeatId);

                if (ph != null)
                {
                    _purchaseHistoryRepository.Remove(ph.Id);

                    _userService.TopUpBalance(userId, area.Price);
                }

                return PurchaseStatus.ReturnTicketSucsess;
            }

            return PurchaseStatus.ReturnTicketFailWithPastEvent;
        }
    }
}
