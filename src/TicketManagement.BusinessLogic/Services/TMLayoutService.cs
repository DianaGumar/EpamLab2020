using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic
{
    internal class TMLayoutService : ITMLayoutService
    {
        private readonly ITMLayoutRepository _tmlayoutRepository;

        internal TMLayoutService(ITMLayoutRepository tmlayoutRepository)
        {
            _tmlayoutRepository = tmlayoutRepository;
        }

        public int RemoveTMLayout(int id)
        {
            return _tmlayoutRepository.Remove(id);
        }

        public List<TMLayout> GetAllTMLayout()
        {
            return _tmlayoutRepository.GetAll().ToList();
        }

        public TMLayout CreateTMLayout(TMLayout obj)
        {
            List<TMLayout> objs = _tmlayoutRepository.GetAll()
               .Where(a => a.VenueId == obj.VenueId && a.Description == obj.Description).ToList();
            return objs.Count == 0 ? _tmlayoutRepository.Create(obj) : objs.ElementAt(0);
        }

        public TMLayout GetTMLayout(int id)
        {
            return _tmlayoutRepository.GetById(id);
        }

        public int UpdateTMLayout(TMLayout obj)
        {
            return _tmlayoutRepository.Update(obj);
        }
    }
}
