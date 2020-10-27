using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Model;
using TicketManagement.Domain;

namespace Ticketmanagement.BusinessLogic.BusinessLogicLayer
{
    internal class TMEventAreasBL : ITMEventAreasBL
    {
        private readonly ITMEventAreaService _tmeventAreaService;
        private readonly ITMEventSeatService _tmeventSeatService;

        internal TMEventAreasBL(ITMEventAreaService tmeventAreaService, ITMEventSeatService tmeventSeatService)
        {
            _tmeventAreaService = tmeventAreaService;
            _tmeventSeatService = tmeventSeatService;
        }

        public void SetTMEventAreaPrice(int areaId, decimal price)
        {
            _tmeventAreaService.SetPrice(areaId, price);
        }

        public TMEventAreaModels GetTMEventArea(int id)
        {
            TMEventArea tmea = _tmeventAreaService.GetTMEventArea(id);

            return CreateTMEventAreaModelsFromTMEventArea(tmea, GetTMEventAreaSeats(tmea.Id));
        }

        public List<TMEventAreaModels> GetAllTMEventAreas()
        {
            var retList = new List<TMEventAreaModels>();

            List<TMEventArea> list = _tmeventAreaService.GetAllTMEventArea().ToList();

            foreach (var item in list)
            {
                retList.Add(CreateTMEventAreaModelsFromTMEventArea(item, GetTMEventAreaSeats(item.Id)));
            }

            return retList;
        }

        public List<TMEventSeatModels> GetTMEventAreaSeats(int tmeventAreaId)
        {
            var seatsNew = new List<TMEventSeatModels>();
            List<TMEventSeat> seats = _tmeventSeatService.GetAllTMEventSeat()
                .Where(s => s.TMEventAreaId == tmeventAreaId).ToList();

            foreach (var item in seats)
            {
                seatsNew.Add(CreateTMEventSeatModelsFromTMEventSeat(item));
            }

            return seatsNew;
        }

        private static TMEventAreaModels CreateTMEventAreaModelsFromTMEventArea(
            TMEventArea obj, List<TMEventSeatModels> seats)
        {
            return new TMEventAreaModels
            {
                TMEventAreaId = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                Price = obj.Price,
                TMEventId = obj.TMEventId,
                CountSeatsX = seats.Max(s => s.Number),
                CountSeatsY = seats.Max(s => s.Row),
            };
        }

        private static TMEventSeatModels CreateTMEventSeatModelsFromTMEventSeat(TMEventSeat obj)
        {
            return new TMEventSeatModels
            {
                TMEventAreaId = obj.TMEventAreaId,
                Id = obj.Id,
                Number = obj.Number,
                Row = obj.Row,
                State = obj.State,
            };
        }
    }
}
