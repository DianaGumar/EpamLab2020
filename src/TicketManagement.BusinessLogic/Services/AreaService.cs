using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class AreaService : AreaRepository, IAreaService
    {
        internal AreaService(string connectStr)
            : base(connectStr)
        {
        }

        public Area CreateArea(Area obj)
        {
            List<Area> objs = GetAll()
               .Where(a => a.TMLayoutId == obj.TMLayoutId &&
               a.CoordX == obj.CoordX &&
               a.CoordY == obj.CoordY).ToList();
            return objs.Count == 0 ? Create(obj) : objs.ElementAt(0);
        }
    }
}
