using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        internal VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public int RemoveVenue(int id)
        {
            return _venueRepository.Remove(id);
        }

        public List<Venue> GetAllVenue()
        {
            return _venueRepository.GetAll().ToList();
        }

        public Venue CreateVenue(Venue obj)
        {
            List<Venue> objs = _venueRepository.GetAll()
                .Where(a => a.Address == obj.Address && a.Description == obj.Description).ToList();
            return objs.Count == 0 ? _venueRepository.Create(obj) : objs.ElementAt(0);
        }
    }
}
