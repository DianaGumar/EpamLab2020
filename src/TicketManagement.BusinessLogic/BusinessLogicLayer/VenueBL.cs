using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.BusinessLogicLayer;
using TicketManagement.DataAccess.Model;

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

        public Area CreateArea(Area area, ref List<Seat> seats)
        {
            area = _areaService.CreateArea(area);
            seats.ForEach(s => s.AreaId = area.Id);
            seats.ForEach(s => s.Id = _seatService.CreateSeat(s).Id);

            return area;
        }

        public TMLayout CreateLayout(TMLayout layout)
        {
            return _tmlayoutService.CreateTMLayout(layout);
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

        public Venue CreateVenue(Venue obj)
        {
            obj = _venueService.CreateVenue(obj);

            // create default layout
            TMLayout layout = _tmlayoutService.CreateTMLayout(new TMLayout { Description = "defailt", VenueId = obj.Id });

            Area area = _areaService.CreateArea(
                new Area { CoordX = 0, CoordY = 0, Description = "all size area", TMLayoutId = layout.Id });

            for (int j = 0; j < obj.Lenght; j++)
            {
                for (int i = 0; i < obj.Weidth; i++)
                {
                    _seatService.CreateSeat(new Seat { Row = j, Number = i, AreaId = area.Id });
                }
            }

            return obj;
        }
    }
}
