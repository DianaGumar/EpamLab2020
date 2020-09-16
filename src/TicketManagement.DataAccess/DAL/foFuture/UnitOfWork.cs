using System.Data.Linq;
using System.Data.SqlClient;

namespace TicketManagement.DataAccess.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _conn;

        public UnitOfWork(string conn)
        {
            _conn = conn;

            Events = new PublicEventRepository(conn);
        }

        public IPublicEventRepository Events { get; private set; }
    }
}
