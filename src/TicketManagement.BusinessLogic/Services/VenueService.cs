using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        internal VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        private static VenueDto ConvertToDto(Venue obj)
        {
            return new VenueDto
            {
                Id = obj.Id,
                Description = obj.Description,
                Address = obj.Address,
                Lenght = obj.Lenght,
                Phone = obj.Phone,
                Weidth = obj.Weidth,
            };
        }

        private static Venue ConvertToEntity(VenueDto obj)
        {
            return new Venue
            {
                Id = obj.Id,
                Description = obj.Description,
                Address = obj.Address,
                Lenght = obj.Lenght,
                Phone = obj.Phone,
                Weidth = obj.Weidth,
            };
        }

        public int RemoveVenue(int id)
        {
            return _venueRepository.Remove(id);
        }

        public List<VenueDto> GetAllVenue()
        {
            List<Venue> objs = _venueRepository.GetAll().ToList();
            var objsDto = new List<VenueDto>();

            foreach (var item in objs)
            {
                objsDto.Add(ConvertToDto(item));
            }

            return objsDto;
        }

        public VenueDto CreateVenue(VenueDto obj)
        {
            List<Venue> objs = _venueRepository.GetAll()
                .Where(a => a.Address == obj.Address
                && a.Description == obj.Description).ToList();

            if (objs.Count == 0)
            {
                Venue e = _venueRepository.Create(ConvertToEntity(obj));
                obj.Id = e.Id;

                return obj;
            }
            else
            {
                return ConvertToDto(objs.ElementAt(0));
            }
        }

        public VenueDto GetVenue(int id)
        {
            return ConvertToDto(_venueRepository.GetById(id));
        }

        public int UpdateVenue(VenueDto obj)
        {
            return _venueRepository.Update(ConvertToEntity(obj));
        }
    }
}
