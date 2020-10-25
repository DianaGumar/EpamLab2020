using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventAreaService : ITMEventAreaService
    {
        private readonly ITMEventAreaRepository _tmeventAreaRepository;

        internal TMEventAreaService(ITMEventAreaRepository tmeventAreaRepository)
        {
            _tmeventAreaRepository = tmeventAreaRepository;
        }

        public List<TMEventArea> GetAllTMEventArea()
        {
            return _tmeventAreaRepository.GetAll().ToList();
        }

        public TMEventArea GetTMEventArea(int id)
        {
            return _tmeventAreaRepository.GetById(id);
        }

        public void SetPrice(int tmeventAreaId, decimal price)
        {
            TMEventArea tmeventArea = _tmeventAreaRepository.GetById(tmeventAreaId);
            tmeventArea.Price = price;
            _tmeventAreaRepository.Update(tmeventArea);
        }
    }
}
