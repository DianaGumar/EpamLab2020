using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public interface ITMEventAreaService
    {
        List<TMEventAreaDto> GetAllTMEventArea();

        TMEventAreaDto GetTMEventArea(int id);

        int UpdateTMEventArea(TMEventAreaDto obj);

        int RemoveTMEventArea(int id);

        TMEventAreaDto CreateTMEventArea(TMEventAreaDto obj);

        List<TMEventSeatDto> GetTMEventSeatsByArea(int tmeventAreaId);

        List<TMEventSeatDto> GetTMEventSeatsByIds(params int[] seatsId);

        int SetTMEventSeatState(int tmeventSeatId, SeatState state);

        int SetTMEventAreaPrice(int areaId, decimal price);

        decimal GetTMEventAreaPrice(int areaId);
    }

    internal class TMEventAreaService : ITMEventAreaService
    {
        private readonly ITMEventAreaRepository _tmeventAreaRepository;
        private readonly ITMEventSeatService _tmeventSeatService;

        public TMEventAreaService(ITMEventAreaRepository tmeventAreaRepository,
            ITMEventSeatService tmeventSeatService)
        {
            _tmeventAreaRepository = tmeventAreaRepository;
            _tmeventSeatService = tmeventSeatService;
        }

        private static TMEventAreaDto ConvertToDto(TMEventArea obj, List<TMEventSeatDto> seats)
        {
            return new TMEventAreaDto
            {
                Id = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                Price = obj.Price,
                TMEventId = obj.TMEventId,
                CountSeatsX = seats.Max(s => s.Number),
                CountSeatsY = seats.Max(s => s.Row),
                TMEventSeats = seats,
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
                TMEventId = obj.TMEventId,
            };
        }

        public TMEventAreaDto CreateTMEventArea(TMEventAreaDto obj)
        {
            return ConvertToDto(
                _tmeventAreaRepository.Create(ConvertToEntity(obj)),
                GetTMEventSeatsByArea(obj.Id));
        }

        public List<TMEventAreaDto> GetAllTMEventArea()
        {
            List<TMEventArea> areas = _tmeventAreaRepository.GetAll().ToList();
            var areasDto = new List<TMEventAreaDto>();

            foreach (var item in areas)
            {
                areasDto.Add(ConvertToDto(item, GetTMEventSeatsByArea(item.Id)));
            }

            return areasDto;
        }

        public TMEventAreaDto GetTMEventArea(int id)
        {
            TMEventArea area = _tmeventAreaRepository.GetById(id);

            return ConvertToDto(area, GetTMEventSeatsByArea(id));
        }

        public int RemoveTMEventArea(int id)
        {
            _tmeventAreaRepository.Remove(id);

            TMEventArea obj = _tmeventAreaRepository.GetById(id);

            return obj == null ? 1 : 0;
        }

        public int UpdateTMEventArea(TMEventAreaDto obj)
        {
            _tmeventAreaRepository.Update(ConvertToEntity(obj));

            return 1;
        }

        public int SetTMEventAreaPrice(int areaId, decimal price)
        {
            TMEventAreaDto tmeventAreaDto = GetTMEventArea(areaId);
            tmeventAreaDto.Price = price;
            return UpdateTMEventArea(tmeventAreaDto);
        }

        public List<TMEventSeatDto> GetTMEventSeatsByArea(int tmeventAreaId)
        {
            return _tmeventSeatService.GetAllTMEventSeat()
                .Where(a => a.TMEventAreaId == tmeventAreaId).ToList();
        }

        public int SetTMEventSeatState(int tmeventSeatId, SeatState state)
        {
            TMEventSeatDto tmeventSeatDto = _tmeventSeatService.GetTMEventSeat(tmeventSeatId);
            tmeventSeatDto.State = state;
            return _tmeventSeatService.UpdateTMEventSeat(tmeventSeatDto);
        }

        public decimal GetTMEventAreaPrice(int areaId)
        {
            return _tmeventAreaRepository.GetById(areaId).Price;
        }

        public List<TMEventSeatDto> GetTMEventSeatsByIds(params int[] seatsId)
        {
            return _tmeventSeatService.GetAllTMEventSeat()
                .Where(s => seatsId.Any(a => a == s.Id)).ToList();
        }
    }
}
