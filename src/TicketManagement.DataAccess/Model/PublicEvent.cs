using System;

namespace TicketManagement.DataAccess.Model
{
    // renamed by code controle
    public class PublicEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int LayoutId { get; set; }

        public DateTime StartEvent { get; set; }

        public DateTime EndEvent { get; set; }

    }
}
