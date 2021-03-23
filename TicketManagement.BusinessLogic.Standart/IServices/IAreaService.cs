﻿using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface IAreaService
    {
        int RemoveArea(int areaId);

        List<AreaDto> GetAllArea();

        AreaDto GetArea(int id);

        AreaDto CreateArea(AreaDto obj);

        int UpdateArea(AreaDto obj);
    }
}