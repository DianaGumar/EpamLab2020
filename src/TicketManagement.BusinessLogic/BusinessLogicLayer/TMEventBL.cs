using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Model;
using TicketManagement.Domain;

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

        private static TMEvent CreateTMEventFromTMEventModels(TMEventModels tmevent)
        {
            return new TMEvent
            {
                Id = tmevent.TMEventId,
                Description = tmevent.Description,
                Name = tmevent.Name,
                EndEvent = tmevent.EndEvent,
                StartEvent = tmevent.StartEvent,
                TMLayoutId = tmevent.TMLayoutId,
                Img = tmevent.Img,
            };
        }

        public List<TMEventModels> GetAllTMEvent()
        {
            List<TMEvent> models = _tmeventService.GetAllTMEvent();
            var tmemodels = new List<TMEventModels>();

            foreach (var model in models)
            {
                tmemodels.Add(CreateTMEventModelsFromTMEvent(model));
            }

            return tmemodels;
        }

        public TMEventModels GetTMEvent(int id)
        {
            TMEvent model = _tmeventService.GetTMEvent(id);

            return CreateTMEventModelsFromTMEvent(model);
        }

        private TMEventModels CreateTMEventModelsFromTMEvent(TMEvent model)
        {
            List<TMEventArea> areas =
                _tmeventAreaService.GetAllTMEventArea().Where(a => a.TMEventId == model.Id).ToList();
            List<TMEventSeat> seats =
                _tmeventSeatService.GetAllTMEventSeat().Where(s => areas.Any(a => a.Id == s.TMEventAreaId)).ToList();

            return new TMEventModels
            {
                Description = model.Description,
                Name = model.Name,
                EndEvent = model.EndEvent,
                StartEvent = model.StartEvent,
                TMLayoutId = model.TMLayoutId,
                AllSeats = seats.Count,
                BusySeats = seats.Where(s => s.State == 1).ToList().Count,
                TMEventId = model.Id,
                Img = model.Img,
                MiddlePriceBySeat = areas.Sum(a => a.Price) / areas.Count,
            };
        }

        public TMEventModels CreateTMEvent(TMEventModels tmevent)
        {
            List<Area> areas = _areaService.GetAllArea().Where(a => a.TMLayoutId == tmevent.TMLayoutId).ToList();
            List<Seat> seats = _seatService.GetAllSeat().Where(s => areas.Any(a => a.Id == s.AreaId)).ToList();

            if (seats.Count <= 0)
            {
                return tmevent;
            }

            TMEvent model = CreateTMEventFromTMEventModels(tmevent);

            tmevent.TMEventId = _tmeventService.CreateTMEvent(model).Id;
            tmevent.AllSeats = seats.Count;
            tmevent.BusySeats = 0;

            return tmevent;
        }

        public int UpdateTMEvent(TMEventModels tmevent)
        {
            TMEvent model = CreateTMEventFromTMEventModels(tmevent);

            return _tmeventService.UpdateTMEvent(model);
        }

        public int DeleteTMEvent(int id)
        {
            return _tmeventService.RemoveTMEvent(id);
        }
    }
}
