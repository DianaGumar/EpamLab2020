using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TicketManagement.BusinessLogic.DAL
{
    public interface IRepository<T>
        where T : class, new()
    {
        T Create(T obj);

        void Remove(T obj);

        int Remove(int id);

        T GetById(object id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        IQueryable<T> Find(Expression<Func<T, bool>> p);
    }
}
