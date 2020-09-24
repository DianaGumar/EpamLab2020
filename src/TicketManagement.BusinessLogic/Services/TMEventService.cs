using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventService : TMEventRepository, ITMEventService
    {
        internal TMEventService(string connectString)
            : base(connectString)
        {
        }

        // null validation property will be in layer upper
        private static bool IsValid(TMEvent obj)
        {
            return obj != null && obj.StartEvent > DateTime.Now.Date && obj.EndEvent > obj.StartEvent;
        }

        TMEvent ITMEventService.CreateEvent(TMEvent obj)
        {
            if (IsValid(obj))
            {
                List<TMEvent> events = GetAll()
                    .Where(item => item.TMLayoutId == obj.TMLayoutId && item.StartEvent == obj.StartEvent).ToList();

                if (events.Count == 0)
                {
                    obj = Create(obj);
                }
            }

            return obj;
        }
    }
}
