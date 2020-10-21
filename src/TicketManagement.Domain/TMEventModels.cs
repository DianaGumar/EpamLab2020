using System;

namespace TicketManagement.Domain
{
    public class TMEventModels
    {
        public int TMEventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TMLayoutId { get; set; }

        public DateTime StartEvent { get; set; }

        public DateTime EndEvent { get; set; }

        public int AllSeats { get; set; }

        public int BusySeats { get; set; }
    }
}
