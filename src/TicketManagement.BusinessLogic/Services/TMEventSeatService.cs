using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventSeatService : ITMEventSeatService
    {
        private readonly ITMEventSeatRepository _tmeventSeatRepository;

        internal TMEventSeatService(ITMEventSeatRepository tmeventSeatRepository)
        {
            _tmeventSeatRepository = tmeventSeatRepository;
        }

        private static TMEventSeatDto ConvertToDto(TMEventSeat obj)
        {
            return new TMEventSeatDto
            {
                Id = obj.Id,
                Number = obj.Number,
                Row = obj.Row,
                State = (SeatState)obj.State,
                TMEventAreaId = obj.TMEventAreaId,
            };
        }

        private static TMEventSeat ConvertToEntity(TMEventSeatDto obj)
        {
            return new TMEventSeat
            {
                Id = obj.Id,
                Number = obj.Number,
                Row = obj.Row,
                State = (int)obj.State,
                TMEventAreaId = obj.TMEventAreaId,
            };
        }

        public int RemoveTMEventSeat(int id)
        {
            return _tmeventSeatRepository.Remove(id);
        }

        public List<TMEventSeatDto> GetAllTMEventSeat()
        {
            List<TMEventSeat> objs = _tmeventSeatRepository.GetAll().ToList();
            var objsDto = new List<TMEventSeatDto>();

            foreach (var item in objs)
            {
                objsDto.Add(ConvertToDto(item));
            }

            return objsDto;
        }

        public int UpdateTMEventSeat(TMEventSeatDto obj)
        {
            return _tmeventSeatRepository.Update(ConvertToEntity(obj));
        }

        public TMEventSeatDto GetTMEventSeat(int id)
        {
            TMEventSeat seat = _tmeventSeatRepository.GetById(id);
            return ConvertToDto(seat);
        }

        public TMEventSeatDto CreateTMEventSeat(TMEventSeatDto obj)
        {
            return ConvertToDto(_tmeventSeatRepository.Create(ConvertToEntity(obj)));
        }
    }
}
