using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface IAreaService : IAreaRepository
    {
        Area CreateArea(Area obj);
    }
}
