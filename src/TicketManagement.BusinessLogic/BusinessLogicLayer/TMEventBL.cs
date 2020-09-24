using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Model;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal class TMEventBL : ITMEventBL
    {
        private readonly IAreaService _areaService;
        private readonly ITMEventService _tmeventService;

        internal TMEventBL(ITMEventService tmeventService, IAreaService areaService)
        {
            _areaService = areaService;
            _tmeventService = tmeventService;
        }

        public TMEvent CreateEvent(TMEvent tmevent)
        {
            List<Area> areas = _areaService.GetAll().Where(a => a.TMLayoutId == tmevent.TMLayoutId).ToList();

            return areas.Count == 0 ? _tmeventService.Create(tmevent) : throw new NotImplementedException();
        }
    }
}
