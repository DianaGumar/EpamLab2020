using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;
using TicketManagement.BusinessLogic.Standart.IServices;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventSeatService : ITMEventSeatService
    {
        private readonly ITMEventSeatRepository _tmeventSeatRepository;

        public TMEventSeatService(ITMEventSeatRepository tmeventSeatRepository)
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
            _tmeventSeatRepository.Remove(id);

            TMEventSeat obj = _tmeventSeatRepository.GetById(id);

            return obj == null ? 1 : 0;
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
            _tmeventSeatRepository.Update(ConvertToEntity(obj));

            return 1;
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
