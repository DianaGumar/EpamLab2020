using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventAreaService : TMEventAreaRepository, ITMEventAreaService
    {
        internal TMEventAreaService(string connectString)
            : base(connectString)
        {
        }

        public void SetPrice(int tmeventAreaId, decimal price)
        {
            TMEventArea tmeventArea = GetById(tmeventAreaId);
            tmeventArea.Price = price;
            Update(tmeventArea);
        }
    }
}
