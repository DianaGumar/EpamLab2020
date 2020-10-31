using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventSeatService : ITMEventSeatService
    {
        private readonly ITMEventSeatRepository _tmeventSeatRepository;

        internal TMEventSeatService(ITMEventSeatRepository tmeventSeatRepository)
        {
            _tmeventSeatRepository = tmeventSeatRepository;
        }

        public int RemoveTMEventSeat(int id)
        {
            return _tmeventSeatRepository.Remove(id);
        }

        public List<TMEventSeat> GetAllTMEventSeat()
        {
            return _tmeventSeatRepository.GetAll().ToList();
        }

        public int UpdateTMEventSeat(TMEventSeat obj)
        {
            return _tmeventSeatRepository.Update(obj);
        }

        public TMEventSeat GetTMEventSeat(int id)
        {
            return _tmeventSeatRepository.GetById(id);
        }

        public TMEventSeat CreateTMEventSeat(TMEventSeat obj)
        {
            return _tmeventSeatRepository.Create(obj);
        }
    }
}
