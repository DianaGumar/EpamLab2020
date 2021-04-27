using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public enum TMEventStatus
    {
        Success = 0,
        DateInPast = 1,
        DateWrongOrder = 2,
        SameByDateObj = 3,
        BusySeatsExists = 4,
        NotExist = 5,
        UnrecognizedError = 6,
        ModelInvalid = 7,
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

            // check for the same dateTime events
            var events = _tmeventRepository.GetAll().Where(item => item.TMLayoutId == obj.TMLayoutId);
            events = events.Where(item => obj.StartEvent >= item.StartEvent && obj.StartEvent <= item.EndEvent);
            var eventsList = events.Where(e => e.Id != obj.Id).ToList();

            if (eventsList.Count > 0)
            {
                return TMEventStatus.SameByDateObj;
            }

            return TMEventStatus.Success;
        }

        public TMEventStatus RemoveTMEvent(int id)
        {
            if (GetTMEvent(id) == null)
            {
                return TMEventStatus.NotExist;
            }

            if (GetTMEventSeatByEvent(id).Where(s => s.State == SeatState.Busy).ToList().Count == 0)
            {
                int count = _tmeventRepository.Remove(id);
                return count > 0 ? TMEventStatus.Success : TMEventStatus.UnrecognizedError;
            }

            return TMEventStatus.BusySeatsExists;
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
            if (obj == null)
            {
                return TMEventStatus.NotExist;
            }

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
            return tmevent == null ? null : ConvertToDto(tmevent, GetTMEventAreaByEvent(id),
                        GetTMEventSeatByEvent(id));
        }

        public TMEventStatus UpdateTMEvent(int id, TMEventDto obj)
        {
            if (obj == null)
            {
                return TMEventStatus.NotExist;
            }

            // check on busy seats
            TMEventDto current = GetTMEvent(id);
            if (current.TMLayoutId != obj.TMLayoutId &&
                GetTMEventSeatByEvent(obj.Id).Where(s => s.State == SeatState.Busy).ToList().Count > 0)
            {
                return TMEventStatus.BusySeatsExists;
            }

            obj.Id = id;
            current = obj;
            TMEvent obje = ConvertToEntity(current);
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
