using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventAreaService
    {
        List<TMEventArea> GetAllTMEventArea();

        void SetPrice(int tmeventAreaId, decimal price);
    }
}
