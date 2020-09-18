using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TicketManagement.IntegrationTests")]

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> // : IDisposable
        where T : class, new()
    {
        T Create(T obj);

        IEnumerable<T> Create(IEnumerable<T> objs);

        int Remove(T obj);

        IEnumerable<T> Remove(IEnumerable<T> objs);

        T GetById(int id);

        IEnumerable<T> GetAll();

        int Update(T obj);

        IEnumerable<T> Update(IEnumerable<T> objs);

        // for future- Find()
    }
}
