using System.Collections.Generic;

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> // : IDisposable
        where T : class, new()
    {
        int Create(T obj);

        int Remove(T obj);

        T GetById(int id);

        IEnumerable<T> GetAll();

        int Update(T obj);

        // for future- Find()
    }
}
