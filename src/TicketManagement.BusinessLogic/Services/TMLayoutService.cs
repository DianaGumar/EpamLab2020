using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    internal class TMLayoutService : ITMLayoutService
    {
        private readonly ITMLayoutRepository _tmlayoutRepository;
        private readonly IVenueService _venueService;

        internal TMLayoutService(ITMLayoutRepository tmlayoutRepository,
            IVenueService venueService)
        {
            _tmlayoutRepository = tmlayoutRepository;
            _venueService = venueService;
        }

        private static TMLayoutDto ConvertToDto(TMLayout obj, VenueDto venue)
        {
            return new TMLayoutDto
            {
                Id = obj.Id,
                Description = obj.Description,
                Venue = venue,
            };
        }

        private static TMLayout ConvertToEntity(TMLayoutDto obj)
        {
            return new TMLayout
            {
                Id = obj.Id,
                Description = obj.Description,
                VenueId = obj.Venue.Id,
            };
        }

        public int RemoveTMLayout(int id)
        {
            return _tmlayoutRepository.Remove(id);
        }

        public List<TMLayoutDto> GetAllTMLayout()
        {
            List<TMLayout> objs = _tmlayoutRepository.GetAll().ToList();
            var objsDto = new List<TMLayoutDto>();

            foreach (var item in objs)
            {
                objsDto.Add(ConvertToDto(item,
                    _venueService.GetVenue(item.VenueId)));
            }

            return objsDto;
        }

        public TMLayoutDto CreateTMLayout(TMLayoutDto obj)
        {
            List<TMLayout> objs = _tmlayoutRepository.GetAll()
               .Where(a => a.VenueId == obj.Venue.Id
               && a.Description == obj.Description).ToList();

            if (objs.Count == 0)
            {
                TMLayout e = _tmlayoutRepository.Create(ConvertToEntity(obj));
                obj.Id = e.Id;

                return obj;
            }
            else
            {
                return ConvertToDto(objs.ElementAt(0),
                    _venueService.GetVenue(objs.ElementAt(0).VenueId));
            }
        }

        public TMLayoutDto GetTMLayout(int id)
        {
            TMLayout obj = _tmlayoutRepository.GetById(id);

            return ConvertToDto(obj, _venueService.GetVenue(obj.VenueId));
        }

        public int UpdateTMLayout(TMLayoutDto obj)
        {
            return _tmlayoutRepository.Update(ConvertToEntity(obj));
        }
    }
}
