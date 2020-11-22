using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace TicketManagement.DataAccess.DAL
{
    public class RepositoryEF<T> : IRepository<T>
        where T : class, new()
    {
        public RepositoryEF(DbContext context)
        {
            Context = context;
            DataBaseSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DataBaseSet { get; }

        public T Create(T obj)
        {
            obj = Context.Set<T>().Add(obj);
            Context.SaveChanges();

            return obj;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> p)
        {
            var query = DataBaseSet.Where(p);

            return query;
        }

        public IEnumerable<T> GetAll()
        {
            return DataBaseSet.ToList();
        }

        public T GetById(object id)
        {
            return DataBaseSet.Find(id);
        }

        public void Remove(T obj)
        {
            DataBaseSet.Remove(obj);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            T obj = DataBaseSet.Find(id);
            DataBaseSet.Remove(obj);

            Context.SaveChanges();

            Context.Database.Log = (e) => { Debug.WriteLine(e); };
        }

        public void Update(T obj)
        {
            Context.Entry(obj).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
