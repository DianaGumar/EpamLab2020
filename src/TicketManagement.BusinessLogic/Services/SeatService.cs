using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        internal SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public int RemoveSeat(int id)
        {
            return _seatRepository.Remove(id);
        }

        public List<Seat> GetAllSeat()
        {
            return _seatRepository.GetAll().ToList();
        }

        public Seat CreateSeat(Seat obj)
        {
            List<Seat> objs = _seatRepository.GetAll()
               .Where(a => a.AreaId == obj.AreaId && a.Row == obj.Row && a.Number == obj.Number).ToList();
            return objs.Count == 0 ? _seatRepository.Create(obj) : objs.ElementAt(0);
        }
    }
}
