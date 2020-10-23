using System;
using System.Collections.Generic;

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> // : IDisposable
        where T : class, new()
    {
        T Create(T obj);

        IEnumerable<T> Create(IEnumerable<T> objs);

        int Remove(T obj);

        int Remove(int id);

        IEnumerable<T> Remove(IEnumerable<T> objs);

        T GetById(int id);

        IEnumerable<T> GetAll();

        int Update(T obj);

        IEnumerable<T> Update(IEnumerable<T> objs);

        IEnumerable<T> Find(Func<T, bool> p);

        // for future- Find()
    }
}
