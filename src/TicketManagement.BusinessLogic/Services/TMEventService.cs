using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.BusinessLogic
{
    internal class TMEventService : ITMEventService
    {
        private readonly ITMEventRepository _tmeventRepository;

        internal TMEventService(ITMEventRepository tmeventRepository)
        {
            _tmeventRepository = tmeventRepository;
        }

        public int RemoveTMEvent(int id)
        {
            return _tmeventRepository.Remove(id);
        }

        public List<TMEvent> GetAllTMEvent()
        {
            return _tmeventRepository.GetAll().ToList();
        }

        TMEvent ITMEventService.CreateTMEvent(TMEvent obj)
        {
            if (obj.StartEvent > DateTime.Now.Date && obj.EndEvent > obj.StartEvent)
            {
                List<TMEvent> objs = _tmeventRepository.GetAll()
                    .Where(item => item.TMLayoutId == obj.TMLayoutId && item.StartEvent == obj.StartEvent).ToList();

                return objs.Count == 0 ? _tmeventRepository.Create(obj) : objs.ElementAt(0);
            }

            return obj;
        }
    }
}
