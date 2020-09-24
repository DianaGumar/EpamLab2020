using TicketManagement.DataAccess.DAL;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventAreaService : ITMEventAreaRepository
    {
        void SetPrice(int tmeventAreaId, decimal price);
    }
}
