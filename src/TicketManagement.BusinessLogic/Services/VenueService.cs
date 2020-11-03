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
        private readonly ITMLayoutService _tmlayoutService;
        private readonly IAreaService _areaService;
        private readonly ISeatService _seatService;

        internal VenueService(IVenueRepository venueRepository, ITMLayoutService tmlayoutService,
            IAreaService areaService, ISeatService seatService)
        {
            _venueRepository = venueRepository;
            _tmlayoutService = tmlayoutService;
            _areaService = areaService;
            _seatService = seatService;
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
                obj = ConvertToDto(e);
                CreateDefaultLayoutForVenue(obj);

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

        public AreaDto CreateArea(AreaDto area, List<SeatDto> seats)
        {
            area = _areaService.CreateArea(area);

            if (area.Id > 0)
            {
                seats.ForEach(s => s.AreaId = area.Id);
                seats.ForEach(s => s.Id = _seatService.CreateSeat(s).Id);
            }

            return area;
        }

        public TMLayoutDto CreateLayout(TMLayoutDto layout,
            List<AreaDto> areas, List<SeatDto> seats)
        {
            layout = _tmlayoutService.CreateTMLayout(layout);

            if (layout.Id > 0)
            {
                foreach (var item in areas)
                {
                    item.TMLayoutId = layout.Id;
                    CreateArea(item, seats.Where(s => s.AreaId == item.Id).ToList());
                }
            }

            return layout;
        }

        public void RemoveLayout(int layoutId)
        {
            // for transaction
            List<AreaDto> areas = _areaService.GetAllArea()
                .Where(a => a.TMLayoutId == layoutId).ToList();
            List<SeatDto> seats = _seatService.GetAllSeat()
                .Where(s => areas.Any(a => a.Id == s.AreaId)).ToList();

            seats.ForEach(s => _seatService.RemoveSeat(s.Id));
            areas.ForEach(a => _areaService.RemoveArea(a.Id));

            try
            {
                _tmlayoutService.RemoveTMLayout(layoutId);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                areas.ForEach(a => _areaService.CreateArea(a));
                seats.ForEach(s => _seatService.CreateSeat(s));
            }
        }

        public List<TMLayoutDto> GetAllLayoutByVenue(int venueId)
        {
            return _tmlayoutService.GetAllTMLayout().Where(l => l.VenueId == venueId).ToList();
        }

        private TMLayoutDto CreateDefaultLayoutForVenue(VenueDto obj)
        {
            var layout = new TMLayoutDto { Description = "defailt", VenueId = obj.Id };

            var area = new AreaDto
            {
                CoordX = 0,
                CoordY = 0,
                Description = "all size area",
            };

            var seats = new List<SeatDto>();

            for (int j = 0; j < obj.Lenght; j++)
            {
                for (int i = 0; i < obj.Weidth; i++)
                {
                    seats.Add(new SeatDto { Row = j, Number = i });
                }
            }

            return CreateLayout(layout, new List<AreaDto> { area }, seats);
        }
    }
}
