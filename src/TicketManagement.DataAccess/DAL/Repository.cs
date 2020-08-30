using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace TicketManagement.DataAccess.DAL
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        //private readonly SqlConnection _conn;
        //// inflexible with db changing
        //public Repository(SqlConnection conn)
        //{
        //    _conn = conn;
        //}

        private readonly string _strConn;

        // inflexible with db changing
        public Repository(string conn)
        {
            _strConn = conn;

            entrails = GetTypesAndPropertiesNames();
        }

        public void Create(T obj)
        {
            // some ADO code (with reflection)

            // fiestly only for event
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            T entity = new T();

            string sql = "select * from " + name + "s where " + entrails.ElementAt(1).Key + "=" + id;

            // from pull conections in future
            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    object[] inside = new object[reader.FieldCount];
                    reader.GetValues(inside);
                    entity = SetValuesByReflection(inside);
                }

                reader.Close();

            }
               
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(T obj)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }


        // todo get out into new class - only reflection


        // for reflectoin work
        private string name = typeof(T).Name;
        private IDictionary<string, string> entrails;


        // some methods for beter work with reflection

        // from list values create full objet
        private T SetValuesByReflection(object[] obj)
        {
            T t = new T();

            Type type = typeof(T);
            var properties = type.GetProperties();

            int i = 0;
            foreach (PropertyInfo info in properties)
            {
                info.SetValue(t, obj[i]);
                i++;
            }

            return t;
        }

        // get info about properties types and names from main type
        private IDictionary<string, string> GetTypesAndPropertiesNames()
        {
            var typesAndNames = new Dictionary<string, string>();

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                typesAndNames.Add(info.Name, info.PropertyType.Name);
            }

            return typesAndNames;
        }



    }
}
