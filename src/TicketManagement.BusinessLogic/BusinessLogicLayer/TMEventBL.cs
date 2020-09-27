using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Model;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal class TMEventBL : ITMEventBL
    {
        private readonly IAreaService _areaService;
        private readonly ISeatService _seatService;
        private readonly ITMEventService _tmeventService;

        internal TMEventBL(ITMEventService tmeventService, IAreaService areaService, ISeatService seatService)
        {
            _seatService = seatService;
            _areaService = areaService;
            _tmeventService = tmeventService;
        }

        public TMEvent CreateTMEvent(TMEvent tmevent)
        {
            List<Area> areas = _areaService.GetAllArea().Where(a => a.TMLayoutId == tmevent.TMLayoutId).ToList();
            List<Seat> seats = _seatService.GetAllSeat().Where(s => areas.Any(a => a.Id == s.AreaId)).ToList();

            return seats.Count > 0 ? _tmeventService.CreateTMEvent(tmevent) : tmevent;
        }
    }
}
