using System.Data.Linq;

namespace TicketManagement.DataAccess.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Events = new EventRepository(context);
        }

        public IEventRepository Events { get; private set; }
    }
}
