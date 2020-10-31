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
        private readonly ITMLayoutService _tmlayoutService;

        internal TMEventService(ITMEventRepository tmeventRepository,
            ITMLayoutService tmlayoutService)
        {
            _tmeventRepository = tmeventRepository;
            _tmlayoutService = tmlayoutService;
        }

        private static TMEventDto ConvertToDto(TMEvent obj, TMLayoutDto tmlayout)
        {
            return new TMEventDto
            {
                Id = obj.Id,
                Description = obj.Description,
                EndEvent = obj.EndEvent,
                Img = obj.Img,
                Name = obj.Name,
                StartEvent = obj.StartEvent,
                TMLayout = tmlayout,
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
                TMLayoutId = obj.TMLayout.Id,
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
                objsDto.Add(ConvertToDto(item,
                    _tmlayoutService.GetTMLayout(item.TMLayoutId)));
            }

            return objsDto;
        }

        public TMEventDto CreateTMEvent(TMEventDto obj)
        {
            if (obj.StartEvent > DateTime.Now.Date && obj.EndEvent > obj.StartEvent)
            {
                List<TMEvent> objs = _tmeventRepository.GetAll()
                    .Where(item => item.TMLayoutId == obj.TMLayout.Id
                    && item.StartEvent == obj.StartEvent).ToList();

                if (objs.Count == 0)
                {
                    TMEvent e = _tmeventRepository.Create(ConvertToEntity(obj));
                    obj.Id = e.Id;

                    return obj;
                }
                else
                {
                    return ConvertToDto(objs.ElementAt(0),
                        _tmlayoutService.GetTMLayout(objs.ElementAt(0).TMLayoutId));
                }
            }

            return obj;
        }

        public TMEventDto GetTMEvent(int id)
        {
            TMEvent tmevent = _tmeventRepository.GetById(id);
            return ConvertToDto(tmevent,
                _tmlayoutService.GetTMLayout(tmevent.TMLayoutId));
        }

        public int UpdateTMEvent(TMEventDto obj)
        {
            return _tmeventRepository.Update(ConvertToEntity(obj));
        }
    }
}
