using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventService : ITMEventService
    {
        private readonly ITMEventRepository _tmeventRepository;
        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;

        internal TMEventService(ITMEventRepository tmeventRepository,
            ITMEventAreaService tmeventAreaService, ITMEventSeatService tmeventSeatService)
        {
            _tmeventRepository = tmeventRepository;
            _tmeventAreaService = tmeventAreaService;
            _tmeventSeatService = tmeventSeatService;
        }

        private static TMEventDto ConvertToDto(TMEvent obj,
            List<TMEventAreaDto> areas, List<TMEventSeatDto> seats)
        {
            return new TMEventDto
            {
                Id = obj.Id,
                Description = obj.Description,
                EndEvent = obj.EndEvent,
                Img = obj.Img,
                Name = obj.Name,
                StartEvent = obj.StartEvent,
                TMLayoutId = obj.TMLayoutId,
                AllSeats = seats.Count,
                BusySeats = seats.Where(s => s.State == SeatState.Busy).ToList().Count,
                MiddlePriceBySeat = areas.Sum(a => a.Price) / areas.Count,
            };
        }

        private static TMEvent ConvertToEntity(TMEventDto obj)
        {
            return new TMEvent
            {
                Id = obj.Id,
                Description = obj.Description,
                EndEvent = obj.EndEvent,
                Img = obj.Img,
                Name = obj.Name,
                StartEvent = obj.StartEvent,
                TMLayoutId = obj.TMLayoutId,
            };
        }

        public int RemoveTMEvent(int id)
        {
            return _tmeventRepository.Remove(id);
        }

        public List<TMEventDto> GetAllTMEvent()
        {
            List<TMEvent> objs = _tmeventRepository.GetAll().ToList();
            var objsDto = new List<TMEventDto>();

            foreach (var item in objs)
            {
                objsDto.Add(ConvertToDto(item, GetTMEventAreaByEvent(item.Id),
                        GetTMEventSeatByEvent(item.Id)));
            }

            return objsDto;
        }

        public TMEventDto CreateTMEvent(TMEventDto obj)
        {
            if (obj.StartEvent > DateTime.Now.Date && obj.EndEvent > obj.StartEvent)
            {
                List<TMEvent> objs = _tmeventRepository.GetAll()
                    .Where(item => item.TMLayoutId == obj.TMLayoutId
                    && item.StartEvent == obj.StartEvent).ToList();

                if (objs.Count == 0)
                {
                    TMEvent e = _tmeventRepository.Create(ConvertToEntity(obj));

                    return ConvertToDto(e, GetTMEventAreaByEvent(obj.Id),
                        GetTMEventSeatByEvent(obj.Id));
                }
                else
                {
                    return ConvertToDto(objs.ElementAt(0), GetTMEventAreaByEvent(obj.Id),
                        GetTMEventSeatByEvent(obj.Id));
                }
            }

            return obj;
        }

        public TMEventDto GetTMEvent(int id)
        {
            TMEvent tmevent = _tmeventRepository.GetById(id);
            return ConvertToDto(tmevent, GetTMEventAreaByEvent(id),
                        GetTMEventSeatByEvent(id));
        }

        public int UpdateTMEvent(TMEventDto obj)
        {
            return _tmeventRepository.Update(ConvertToEntity(obj));
        }

        public List<TMEventAreaDto> GetTMEventAreaByEvent(int eventId)
        {
            return _tmeventAreaService.GetAllTMEventArea()
                .Where(a => a.TMEventId == eventId).ToList();
        }

        public List<TMEventSeatDto> GetTMEventSeatByEvent(int eventId)
        {
            List<TMEventAreaDto> areas = GetTMEventAreaByEvent(eventId);

            return _tmeventSeatService.GetAllTMEventSeat()
                .Where(s => areas.Any(a => a.Id == s.TMEventAreaId)).ToList();
        }
    }
}
