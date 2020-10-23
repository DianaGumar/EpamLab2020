using System;

namespace TicketManagement.DataAccess.Model
{
    // renamed by code controle
    public class TMEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TMLayoutId { get; set; }

        public DateTime StartEvent { get; set; }

        public DateTime EndEvent { get; set; }
    }
}
