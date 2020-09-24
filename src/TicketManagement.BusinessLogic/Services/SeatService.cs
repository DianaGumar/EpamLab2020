using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class SeatService : SeatRepository, ISeatService
    {
        internal SeatService(string connectStr)
            : base(connectStr)
        {
        }

        public Seat CreateSeat(Seat obj)
        {
            List<Seat> objs = GetAll()
               .Where(a => a.AreaId == obj.AreaId && a.Row == obj.Row && a.Number == obj.Number).ToList();
            return objs.Count == 0 ? Create(obj) : objs.ElementAt(0);
        }
    }
}
