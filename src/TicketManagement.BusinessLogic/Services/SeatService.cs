using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IAreaService _areaService;

        internal SeatService(ISeatRepository seatRepository, IAreaService areaService)
        {
            _seatRepository = seatRepository;
            _areaService = areaService;
        }

        private static SeatDto ConvertToDto(Seat obj, AreaDto area)
        {
            return new SeatDto
            {
                Id = obj.Id,
                Area = area,
                Number = obj.Number,
                Row = obj.Row,
            };
        }

        private static Seat ConvertToEntity(SeatDto obj)
        {
            return new Seat
            {
                Id = obj.Id,
                AreaId = obj.Area.Id,
                Number = obj.Number,
                Row = obj.Row,
            };
        }

        public SeatDto CreateSeat(SeatDto obj)
        {
            List<Seat> objs = _seatRepository.GetAll()
               .Where(a => a.AreaId == obj.Area.Id
               && a.Row == obj.Row && a.Number == obj.Number).ToList();

            return objs.Count == 0
                ? ConvertToDto(
                    _seatRepository.Create(ConvertToEntity(obj)),
                    _areaService.GetArea(obj.Area.Id))
                : ConvertToDto(objs.ElementAt(0),
                    _areaService.GetArea(objs.ElementAt(0).AreaId));
        }

        public int RemoveSeat(int id)
        {
            return _seatRepository.Remove(id);
        }

        public List<SeatDto> GetAllSeat()
        {
            List<Seat> seats = _seatRepository.GetAll().ToList();
            var seatsDto = new List<SeatDto>();

            foreach (var item in seats)
            {
                seatsDto.Add(ConvertToDto(item,
                    _areaService.GetArea(item.AreaId)));
            }

            return seatsDto;
        }

        public SeatDto GetSeat(int id)
        {
            Seat seat = _seatRepository.GetById(id);
            var seatDto = ConvertToDto(seat,
                _areaService.GetArea(seat.AreaId));

            return seatDto;
        }

        public int UpdateSeat(SeatDto obj)
        {
            return _seatRepository.Update(ConvertToEntity(obj));
        }
    }
}
