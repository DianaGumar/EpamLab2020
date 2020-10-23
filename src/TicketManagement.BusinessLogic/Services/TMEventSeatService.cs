using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

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

        public void SetState(int tmeventSeatId, int state)
        {
            TMEventSeat tmeventSeat = _tmeventSeatRepository.GetById(tmeventSeatId);
            tmeventSeat.State = state;
            _tmeventSeatRepository.Update(tmeventSeat);
        }
    }
}
