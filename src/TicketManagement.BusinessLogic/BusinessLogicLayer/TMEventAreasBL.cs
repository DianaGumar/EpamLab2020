using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal class TMEventAreasBL : ITMEventAreasBL
    {
        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;

        internal TMEventAreasBL(ITMEventAreaService tmeventAreaService, ITMEventSeatService tmeventSeatService)
        {
            _tmeventAreaService = tmeventAreaService;
            _tmeventSeatService = tmeventSeatService;
        }

        public int SetTMEventAreaPrice(int areaId, decimal price)
        {
            TMEventArea obj = _tmeventAreaService.GetTMEventArea(areaId);
            obj.Price = price;
            return _tmeventAreaService.UpdateTMEventArea(obj);
        }

        public TMEventAreaDto GetTMEventArea(int id)
        {
            TMEventArea tmea = _tmeventAreaService.GetTMEventArea(id);

            return CreateTMEventAreaDtoFromTMEventArea(tmea, GetTMEventSeats(tmea.Id));
        }

        public List<TMEventAreaDto> GetAllTMEventAreas()
        {
            var retList = new List<TMEventAreaDto>();

            List<TMEventArea> list = _tmeventAreaService.GetAllTMEventArea().ToList();

            foreach (var item in list)
            {
                retList.Add(CreateTMEventAreaDtoFromTMEventArea(item, GetTMEventSeats(item.Id)));
            }

            return retList;
        }

        public List<TMEventSeatDto> GetTMEventSeats(int tmeventAreaId)
        {
            var seatsNew = new List<TMEventSeatDto>();
            List<TMEventSeat> seats = _tmeventSeatService.GetAllTMEventSeat()
                .Where(s => s.TMEventAreaId == tmeventAreaId).ToList();

            foreach (var item in seats)
            {
                seatsNew.Add(CreateTMEventSeatDtoFromTMEventSeat(item));
            }

            return seatsNew;
        }

        public int SetTMEventSeatState(int tmeventSeatId, SeatState state)
        {
            TMEventSeat tmeventSeat = _tmeventSeatService.GetTMEventSeat(tmeventSeatId);
            tmeventSeat.State = (int)state;
            return _tmeventSeatService.UpdateTMEventSeat(tmeventSeat);
        }
    }
}
