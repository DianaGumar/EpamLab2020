﻿using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    // servise work like the validator and the convertator Entity to DTO
    internal class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        private static AreaDto ConvertToDto(Area obj)
        {
            return new AreaDto
            {
                Id = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                TMLayoutId = obj.TMLayoutId,
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
                TMLayoutId = obj.TMLayoutId,
            };
        }

        public List<AreaDto> GetAllArea()
        {
            List<Area> areas = _areaRepository.GetAll().ToList();
            var areasDto = new List<AreaDto>();

            foreach (var item in areas)
            {
                areasDto.Add(ConvertToDto(item));
            }

            return areasDto;
        }

        public int RemoveArea(int areaId)
        {
            // _seatService.RemoveSeatInArea(areaId)
            _areaRepository.Remove(areaId);

            Area area = _areaRepository.GetById(areaId);

            return area == null ? 1 : 0;
        }

        public AreaDto CreateArea(AreaDto obj)
        {
            // find the same in schema
            List<Area> objs = _areaRepository.GetAll()
               .Where(a => a.TMLayoutId == obj.TMLayoutId &&
               a.CoordX == obj.CoordX &&
               a.CoordY == obj.CoordY).ToList();

            if (objs.Count == 0)
            {
                Area createdObj = ConvertToEntity(obj);
                obj.Id = createdObj.Id;

                // _seatService.CreateSeatInArea(obj.Seats, obj.AreaId)
                return obj;
            }

            return ConvertToDto(objs.ElementAt(0));
        }

        // to do validation, check clone and create domain model in dafferent methods
        public AreaDto GetArea(int id)
        {
            Area area = _areaRepository.GetById(id);
            var areaDto = ConvertToDto(area);

            return areaDto;
        }

        public int UpdateArea(AreaDto obj)
        {
            // _seatService.UpdateSeatInArea(obj.Seats, obj.AreaId)
            _areaRepository.Update(ConvertToEntity(obj));

            return 1;
        }
    }
}