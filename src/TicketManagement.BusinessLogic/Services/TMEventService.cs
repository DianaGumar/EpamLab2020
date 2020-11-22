using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public enum TMEventStatus
    {
        Success = 0,
        DateInPast = 1,
        DateWrongOrder = 2,
        SameByDateObj = 3,
    }

    public interface ITMEventService
    {
        int RemoveTMEvent(int id);

        TMEventDto GetTMEvent(int id);

        List<TMEventDto> GetAllTMEvent();

        List<TMEventDto> GetAllRelevantTMEvent();

        TMEventStatus CreateTMEvent(TMEventDto obj);

        TMEventStatus UpdateTMEvent(TMEventDto obj);

        List<TMEventSeatDto> GetTMEventSeatByEvent(int eventId);

        List<TMEventAreaDto> GetTMEventAreaByEvent(int eventId);
    }

    internal class TMEventService : ITMEventService
    {
        private readonly ITMEventRepository _tmeventRepository;

        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;

        public TMEventService(ITMEventRepository tmeventRepository, ITMEventAreaService tmeventAreaService,
            ITMEventSeatService tmeventSeatService)
        {
            _tmeventRepository = tmeventRepository;
            _tmeventAreaService = tmeventAreaService;
            _tmeventSeatService = tmeventSeatService;
        }

        private static TMEventDto ConvertToDto(TMEvent obj,
            List<TMEventAreaDto> areas, List<TMEventSeatDto> seats)
        {
            if (obj == null)
            {
                return null;
            }

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
                MiddlePriceBySeat = areas.Count > 0 ? areas.Sum(a => a.Price) / areas.Count : 0,
            };
        }

        private static TMEvent ConvertToEntity(TMEventDto obj)
        {
            if (obj == null)
            {
                return null;
            }

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

        private TMEventStatus IsValid(TMEvent obj)
        {
            if (obj.StartEvent < DateTime.Now.Date)
            {
                return TMEventStatus.DateInPast;
            }

            if (obj.EndEvent < obj.StartEvent)
            {
                return TMEventStatus.DateWrongOrder;
            }

            if (_tmeventRepository.GetAll().Where(item => item.TMLayoutId == obj.TMLayoutId
                    && obj.StartEvent >= item.StartEvent
                    && obj.StartEvent <= item.EndEvent).ToList().Count > 0)
            {
                return TMEventStatus.SameByDateObj;
            }

            return TMEventStatus.Success;
        }

        public int RemoveTMEvent(int id)
        {
            if (GetTMEventSeatByEvent(id).Where(s => s.State == SeatState.Busy).ToList().Count == 0)
            {
                _tmeventRepository.Remove(id);

                TMEvent obj = _tmeventRepository.GetById(id);

                return obj == null ? 1 : 0;
            }

            return 0;
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

        public List<TMEventDto> GetAllRelevantTMEvent()
        {
            List<TMEvent> objs = _tmeventRepository.GetAll().ToList();
            List<TMEventAreaDto> areas = _tmeventAreaService.GetAllTMEventArea();

            var objsDto = new List<TMEventDto>();

            foreach (var item in objs)
            {
                if (areas.Where(a => a.TMEventId == item.Id && a.Price == 0).ToList().Count == 0)
                {
                    objsDto.Add(ConvertToDto(item, GetTMEventAreaByEvent(item.Id),
                        GetTMEventSeatByEvent(item.Id)));
                }
            }

            return objsDto;
        }

        public TMEventStatus CreateTMEvent(TMEventDto obj)
        {
            TMEvent obje = ConvertToEntity(obj);

            TMEventStatus result = IsValid(obje);

            if (result == TMEventStatus.Success)
            {
                obje = _tmeventRepository.Create(obje);

                obj.Id = obje.Id;
            }

            return result;
        }

        public TMEventDto GetTMEvent(int id)
        {
            TMEvent tmevent = _tmeventRepository.GetById(id);
            return ConvertToDto(tmevent, GetTMEventAreaByEvent(id),
                        GetTMEventSeatByEvent(id));
        }

        public TMEventStatus UpdateTMEvent(TMEventDto obj)
        {
            TMEvent obje = ConvertToEntity(obj);

            TMEventStatus result = IsValid(obje);

            if (result == TMEventStatus.Success)
            {
                _tmeventRepository.Update(obje);
            }

            return result;
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
