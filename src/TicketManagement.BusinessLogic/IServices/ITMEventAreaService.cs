﻿using System.Collections.Generic;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal interface ITMEventAreaService
    {
        List<TMEventArea> GetAllTMEventArea();

        TMEventArea GetTMEventArea(int id);

        int UpdateTMEventArea(TMEventArea obj);
    }
}
