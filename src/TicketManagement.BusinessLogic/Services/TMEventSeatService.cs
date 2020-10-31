using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventSeatService : ITMEventSeatService
    {
        private readonly ITMEventSeatRepository _tmeventSeatRepository;
        private readonly ITMEventAreaService _tmeventAreaService;

        internal TMEventSeatService(ITMEventSeatRepository tmeventSeatRepository,
            ITMEventAreaService tmeventAreaService)
        {
            _tmeventSeatRepository = tmeventSeatRepository;
            _tmeventAreaService = tmeventAreaService;
        }

        private static TMEventSeatDto ConvertToDto(TMEventSeat obj, TMEventAreaDto tmeventarea)
        {
            return new TMEventSeatDto
            {
                Id = obj.Id,
                Number = obj.Number,
                Row = obj.Row,
                State = (SeatState)obj.State,
                TMEventArea = tmeventarea,
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
                TMEventAreaId = obj.TMEventArea.Id,
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
                objsDto.Add(ConvertToDto(item,
                    _tmeventAreaService.GetTMEventArea(item.TMEventAreaId)));
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
            return ConvertToDto(seat, _tmeventAreaService.GetTMEventArea(seat.TMEventAreaId));
        }

        public TMEventSeatDto CreateTMEventSeat(TMEventSeatDto obj)
        {
            return ConvertToDto(_tmeventSeatRepository.Create(ConvertToEntity(obj)),
                _tmeventAreaService.GetTMEventArea(obj.TMEventArea.Id));
        }
    }
}
