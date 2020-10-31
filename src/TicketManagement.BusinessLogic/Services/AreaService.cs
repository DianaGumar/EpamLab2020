using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    // servise work -- is the validation and the convertation dbModel in Domain model
    internal class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ITMLayoutService _tmlayoutService;

        internal AreaService(IAreaRepository areaRepository,
            ITMLayoutService tmlayoutService)
        {
            _areaRepository = areaRepository;
            _tmlayoutService = tmlayoutService;
        }

        private static AreaDto ConvertToDto(Area obj, TMLayoutDto layout)
        {
            return new AreaDto
            {
                Id = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                TMLayout = layout,
            };
        }

        private static Area ConvertToEntity(AreaDto obj)
        {
            return new Area
            {
                Id = obj.Id,
                Description = obj.Description,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                TMLayoutId = obj.TMLayout.Id,
            };
        }

        public List<AreaDto> GetAllArea()
        {
            List<Area> areas = _areaRepository.GetAll().ToList();
            var areasDto = new List<AreaDto>();

            foreach (var item in areas)
            {
                areasDto.Add(ConvertToDto(item,
                    _tmlayoutService.GetTMLayout(item.TMLayoutId)));
            }

            return areasDto;
        }

        public int RemoveArea(int areaId)
        {
            // _seatService.RemoveSeatInArea(areaId)
            return _areaRepository.Remove(areaId);
        }

        public AreaDto CreateArea(AreaDto obj)
        {
            // find the same in schema
            List<Area> objs = _areaRepository.GetAll()
               .Where(a => a.TMLayoutId == obj.TMLayout.Id &&
               a.CoordX == obj.CoordX &&
               a.CoordY == obj.CoordY).ToList();

            if (objs.Count == 0)
            {
                Area createdObj = ConvertToEntity(obj);
                obj.Id = createdObj.Id;

                // _seatService.CreateSeatInArea(obj.Seats, obj.AreaId)
                return obj;
            }

            return ConvertToDto(objs.ElementAt(0),
                _tmlayoutService.GetTMLayout(objs.ElementAt(0).TMLayoutId));
        }

        // to do validation, check clone and create domain model in dafferent methods
        public AreaDto GetArea(int id)
        {
            Area area = _areaRepository.GetById(id);
            var areaDto = ConvertToDto(area,
                _tmlayoutService.GetTMLayout(area.TMLayoutId));

            return areaDto;
        }

        public int UpdateArea(AreaDto obj)
        {
            // _seatService.UpdateSeatInArea(obj.Seats, obj.AreaId)
            return _areaRepository.Update(ConvertToEntity(obj));
        }
    }
}
