using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal class TMEventBL : ITMEventBL
    {
        private readonly IAreaService _areaService;
        private readonly ISeatService _seatService;

        private readonly ITMEventService _tmeventService;
        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;

        internal TMEventBL(
            ITMEventService tmeventService,
            IAreaService areaService,
            ISeatService seatService,
            ITMEventAreaService tmeventAreaService,
            ITMEventSeatService tmeventSeatService)
        {
            _seatService = seatService;
            _areaService = areaService;
            _tmeventService = tmeventService;
            _tmeventAreaService = tmeventAreaService;
            _tmeventSeatService = tmeventSeatService;
        }

        public List<TMEventDto> GetAllTMEvent()
        {
            return _tmeventService.GetAllTMEvent();
        }

        public TMEventDto GetTMEvent(int id)
        {
            return _tmeventService.GetTMEvent(id);
        }

        public TMEventDto CreateTMEvent(TMEventDto tmevent)
        {
            List<Area> areas = _areaService.GetAllArea()
                .Where(a => a.TMLayoutId == tmevent.TMLayoutId).ToList();
            List<Seat> seats = _seatService.GetAllSeat()
                .Where(s => areas.Any(a => a.Id == s.AreaId)).ToList();

            if (seats.Count <= 0)
            {
                return tmevent;
            }

            tmevent.AllSeats = seats.Count;
            tmevent.BusySeats = 0;

            return _tmeventService.CreateTMEvent(tmevent);
        }

        public int UpdateTMEvent(TMEventDto tmevent)
        {
            return _tmeventService.UpdateTMEvent(tmevent);
        }

        public int DeleteTMEvent(int id)
        {
            return _tmeventService.RemoveTMEvent(id);
        }
    }
}
