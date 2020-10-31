using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain;
using TicketManagement.Domain.DTO;

namespace Ticketmanagement.BusinessLogic.POCO
{
    internal class TMEventPoco
    {

        internal TMEventPoco()
        {

        }

        public TMEventDto TMEventDto { get; set; }

        public TMEventAreaDto TMEventAreaDto { get; set; }
    }
}
