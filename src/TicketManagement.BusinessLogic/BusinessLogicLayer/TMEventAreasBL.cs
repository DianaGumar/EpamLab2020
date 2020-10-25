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

        internal TMEventAreasBL(ITMEventAreaService tmeventAreaService)
        {
            _tmeventAreaService = tmeventAreaService;
        }

        public void SetTMEventAreaPrice(int areaId, decimal price)
        {
            _tmeventAreaService.SetPrice(areaId, price);
        }

        public TMEventAreaModels GetTMEventArea(int id)
        {
            return CreateTMEventAreaModelsFromTMEventArea(_tmeventAreaService.GetTMEventArea(id));
        }

        public List<TMEventAreaModels> GetAllTMEventAreas()
        {
            var retList = new List<TMEventAreaModels>();

            List<TMEventArea> list = _tmeventAreaService.GetAllTMEventArea().ToList();

            foreach (var item in list)
            {
                retList.Add(CreateTMEventAreaModelsFromTMEventArea(item));
            }

            return retList;
        }

        private static TMEventAreaModels CreateTMEventAreaModelsFromTMEventArea(TMEventArea obj)
        {
            return new TMEventAreaModels
            {
                TMEventAreaId = obj.Id,
                CoordX = obj.CoordX,
                CoordY = obj.CoordY,
                Description = obj.Description,
                Price = obj.Price,
                TMEventId = obj.TMEventId,
            };
        }
    }
}
