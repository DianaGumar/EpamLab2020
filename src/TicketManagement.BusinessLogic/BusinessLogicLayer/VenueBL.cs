using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.BusinessLogicLayer
{
    internal class VenueBL : IVenueBL
    {
        private readonly IVenueService _venueService;
        private readonly ITMLayoutService _tmlayoutService;
        private readonly IAreaService _areaService;
        private readonly ISeatService _seatService;

        internal VenueBL(IVenueService venueService, ITMLayoutService tmlayoutService,
            IAreaService areaService, ISeatService seatService)
        {
            _venueService = venueService;
            _tmlayoutService = tmlayoutService;
            _areaService = areaService;
            _seatService = seatService;
        }

        public AreaDto CreateArea(AreaDto area, List<Seat> seats)
        {
            area = CreateAreaModelsFromArea(
                _areaService.CreateArea(CreateAreaFromAreaModels(area)));
            seats.ForEach(s => s.AreaId = area.AreaId);
            seats.ForEach(s => s.Id = _seatService.CreateSeat(s).Id);

            return area;
        }

        public TMLayoutDto CreateLayout(TMLayoutDto layout)
        {
            return CreateTMLayoutModelsFromTMLayout(
                _tmlayoutService.CreateTMLayout(CreateTMLayoutFromTMLayoutModels(layout)));
        }

        public void RemoveLayout(int layoutId)
        {
            // for transaction
            List<Area> areas = _areaService.GetAllArea().Where(a => a.TMLayoutId == layoutId).ToList();
            List<Seat> seats = _seatService.GetAllSeat().Where(s => areas.Any(a => a.Id == s.AreaId)).ToList();

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

        public VenueDto CreateVenue(VenueDto obj)
        {
            obj = CreateVenueModelsFromVenue(_venueService
                .CreateVenue(CreateVenueFromVenueModels(obj)));

            // create default layout
            TMLayout layout = _tmlayoutService.CreateTMLayout(
                new TMLayout { Description = "defailt", VenueId = obj.VenueId });

            Area area = _areaService.CreateArea(
                new Area
                {
                    CoordX = 0,
                    CoordY = 0,
                    Description = "all size area",
                    TMLayoutId = layout.Id,
                });

            for (int j = 0; j < obj.Lenght; j++)
            {
                for (int i = 0; i < obj.Weidth; i++)
                {
                    _seatService.CreateSeat(new Seat { Row = j, Number = i, AreaId = area.Id });
                }
            }

            return obj;
        }

        public List<VenueDto> GetAllVenues()
        {
            var retList = new List<VenueModels>();

            List<Venue> list = _venueService.GetAllVenue().ToList();

            foreach (var item in list)
            {
                retList.Add(CreateVenueModelsFromVenue(item));
            }

            return retList;
        }

        public List<TMLayoutDto> GetAllLayouts()
        {
            List<TMLayoutDto> list = _tmlayoutService.GetAllTMLayout().ToList();

            return retList;
        }
    }
}
