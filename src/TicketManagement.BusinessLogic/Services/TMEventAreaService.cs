using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventAreaService : ITMEventAreaService
    {
        private readonly ITMEventAreaRepository _tmeventAreaRepository;
        private readonly ITMEventService _tmeventService;

        internal TMEventAreaService(ITMEventAreaRepository tmeventAreaRepository,
            ITMEventService tmeventService)
        {
            _tmeventAreaRepository = tmeventAreaRepository;
            _tmeventService = tmeventService;
        }

        private static TMEventAreaDto ConvertToDto(TMEventArea obj, TMEventDto tmevent)
        {
            return new TMEventAreaDto
            {
                Id = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                Price = obj.Price,
                TMEvent = tmevent,
            };
        }

        private static TMEventArea ConvertToEntity(TMEventAreaDto obj)
        {
            return new TMEventArea
            {
                Id = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                Price = obj.Price,
                TMEventId = obj.TMEvent.Id,
            };
        }

        public TMEventAreaDto CreateTMEventArea(TMEventAreaDto obj)
        {
            return ConvertToDto(_tmeventAreaRepository.Create(ConvertToEntity(obj)),
                _tmeventService.GetTMEvent(obj.TMEvent.Id));
        }

        public List<TMEventAreaDto> GetAllTMEventArea()
        {
            List<TMEventArea> areas = _tmeventAreaRepository.GetAll().ToList();
            var areasDto = new List<TMEventAreaDto>();

            foreach (var item in areas)
            {
                areasDto.Add(ConvertToDto(item, _tmeventService.GetTMEvent(item.TMEventId)));
            }

            return areasDto;
        }

        public TMEventAreaDto GetTMEventArea(int id)
        {
            TMEventArea area = _tmeventAreaRepository.GetById(id);

            return ConvertToDto(area, _tmeventService.GetTMEvent(area.TMEventId));
        }

        public int RemoveTMEventArea(int id)
        {
            return _tmeventAreaRepository.Remove(id);
        }

        public int UpdateTMEventArea(TMEventAreaDto obj)
        {
            return _tmeventAreaRepository.Update(ConvertToEntity(obj));
        }
    }
}
