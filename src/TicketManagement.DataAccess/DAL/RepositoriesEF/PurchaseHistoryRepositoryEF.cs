using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class PurchaseHistoryRepositoryEF : RepositoryEF<PurchaseHistory>, IPurchaseHistoryRepository
    {
        public PurchaseHistoryRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
