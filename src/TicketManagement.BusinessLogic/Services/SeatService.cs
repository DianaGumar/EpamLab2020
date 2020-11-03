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

        internal SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        private static SeatDto ConvertToDto(Seat obj)
        {
            return new SeatDto
            {
                Id = obj.Id,
                AreaId = obj.AreaId,
                Number = obj.Number,
                Row = obj.Row,
            };
        }

        private static Seat ConvertToEntity(SeatDto obj)
        {
            return new Seat
            {
                Id = obj.Id,
                AreaId = obj.AreaId,
                Number = obj.Number,
                Row = obj.Row,
            };
        }

        public SeatDto CreateSeat(SeatDto obj)
        {
            List<Seat> objs = _seatRepository.GetAll()
               .Where(a => a.AreaId == obj.AreaId
               && a.Row == obj.Row && a.Number == obj.Number).ToList();

            return objs.Count == 0
                ? ConvertToDto(_seatRepository.Create(ConvertToEntity(obj)))
                : ConvertToDto(objs.ElementAt(0));
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
                seatsDto.Add(ConvertToDto(item));
            }

            return seatsDto;
        }

        public SeatDto GetSeat(int id)
        {
            Seat seat = _seatRepository.GetById(id);
            var seatDto = ConvertToDto(seat);

            return seatDto;
        }

        public int UpdateSeat(SeatDto obj)
        {
            return _seatRepository.Update(ConvertToEntity(obj));
        }
    }
}
