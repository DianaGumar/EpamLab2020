using System;
using System.Collections.Generic;

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> //: IDisposable
        where T : class, new()
    {
        void Create(T obj);

        void Remove(T obj);

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        // for future- Find()
    }
}
