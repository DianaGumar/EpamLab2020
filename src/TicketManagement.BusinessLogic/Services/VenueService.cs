using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class VenueService : VenueRepository, IVenueService
    {
        internal VenueService(string connectStr)
            : base(connectStr)
        {
        }

        public Venue CreateVenue(Venue obj)
        {
            List<Venue> objs = GetAll()
                .Where(a => a.Address == obj.Address && a.Description == obj.Description).ToList();
            return objs.Count == 0 ? Create(obj) : objs.ElementAt(0);
        }
    }
}
