using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        internal AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public List<Area> GetAllArea()
        {
            return _areaRepository.GetAll().ToList();
        }

        public int RemoveArea(int id)
        {
            return _areaRepository.Remove(id);
        }

        public Area CreateArea(Area obj)
        {
            List<Area> objs = _areaRepository.GetAll()
               .Where(a => a.TMLayoutId == obj.TMLayoutId &&
               a.CoordX == obj.CoordX &&
               a.CoordY == obj.CoordY).ToList();
            return objs.Count == 0 ? _areaRepository.Create(obj) : objs.ElementAt(0);
        }
    }
}
