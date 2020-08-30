using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DataAccess.DAL
{
    public interface IRepository<T> where T : class
    {
        void Add(T obj);
        void Remove(T obj);
        T Get(int id);
        IEnumerable<T> GetAll();

        //for future- Find()

    }
}
