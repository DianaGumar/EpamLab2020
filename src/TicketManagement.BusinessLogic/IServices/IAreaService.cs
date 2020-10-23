using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface IAreaService
    {
        int RemoveArea(int id);

        List<Area> GetAllArea();

        Area CreateArea(Area obj);
    }
}
