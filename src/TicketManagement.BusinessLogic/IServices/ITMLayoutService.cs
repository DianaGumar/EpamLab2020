using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMLayoutService
    {
        int RemoveTMLayout(int id);

        List<TMLayout> GetAllTMLayout();

        TMLayout CreateTMLayout(TMLayout obj);
    }
}
