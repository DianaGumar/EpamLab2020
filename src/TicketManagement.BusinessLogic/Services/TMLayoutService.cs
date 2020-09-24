using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMLayoutService : TMLayoutRepository, ITMLayoutService
    {
        internal TMLayoutService(string connectStr)
            : base(connectStr)
        {
        }

        public TMLayout CreateTMLayout(TMLayout obj)
        {
            List<TMLayout> objs = GetAll()
               .Where(a => a.VenueId == obj.VenueId && a.Description == obj.Description).ToList();
            return objs.Count == 0 ? Create(obj) : objs.ElementAt(0);
        }
    }
}
