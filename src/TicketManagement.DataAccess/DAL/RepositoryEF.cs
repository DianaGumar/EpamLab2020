using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TicketManagement.DataAccess.DAL
{
    public class RepositoryEF<T> : IRepository<T>
        where T : class, new()
    {
        public RepositoryEF(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; }

        public T Create(T obj)
        {
            // what will be returned ?
            return DbContext.Set<T>().Add(obj);
        }

        public IEnumerable<T> Create(IEnumerable<T> objs)
        {
            return DbContext.Set<T>().AddRange(objs);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> p)
        {
            return DbContext.Set<T>().Where(p);
        }

        public IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public int Remove(T obj)
        {
            T robj = DbContext.Set<T>().Remove(obj);
            return robj == null ? 0 : 1;
        }

        public int Remove(int id)
        {
            T obj = GetById(id);
            return Remove(obj);
        }

        public IEnumerable<T> Remove(IEnumerable<T> objs)
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Update(IEnumerable<T> objs)
        {
            throw new NotImplementedException();
        }
    }
}
