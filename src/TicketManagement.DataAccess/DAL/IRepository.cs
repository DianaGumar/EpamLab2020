using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> // : IDisposable
        where T : class, new()
    {
        T Create(T obj);

        void Remove(T obj);

        void Remove(int id);

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        IQueryable<T> Find(Expression<Func<T, bool>> p);
    }
}
