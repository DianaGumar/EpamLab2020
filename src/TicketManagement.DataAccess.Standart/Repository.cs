using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        // for reflectoin work
        private readonly string _objPropertiesNames;

        private readonly string _objName = typeof(T).Name;

        private readonly IDictionary<string, string> _objPropertiesInfo;

        public Repository(string conn)
        {
            StrConn = conn;

            _objPropertiesInfo = GetPropertiesNamesAndTypes();
            _objPropertiesNames = string.Join(",", _objPropertiesInfo.Keys.ToArray());
        }

        protected string StrConn { get; }

        // some ADO code methods (with reflection)
        public T Create(T obj)
        {
            // will be care about null checking
            IEnumerable<object> values = GetPropertiesValues(obj);

            ICollection<string> list = _objPropertiesInfo.Keys.Skip(1).ToList();

            string objPropertiesNamesWithoutId = string.Join(",", list.ToArray());

            StringBuilder sb = new StringBuilder();

            string split = "";

            for (int i = 1; i < _objPropertiesInfo.Count; i++)
            {
                sb.Append(split);
                split = ",";
                sb.Append('@');
                sb.Append(_objPropertiesInfo.ElementAt(i).Key);
            }

            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            for (int i = 1; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue('@' + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            command.CommandText = $"insert into {_objName} ({objPropertiesNamesWithoutId}) OUTPUT INSERTED.ID values({sb});";

            conn.Open();
            var idNewObj = (int)command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj);
            return obj;
        }

        public IQueryable<T> Create(IEnumerable<T> objs)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            T entity = new T();

            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.Add("@objPKValue", SqlDbType.Int).Value = id;

            command.CommandText =
                $"select {_objPropertiesNames} from {_objName} where {_objPropertiesInfo.ElementAt(0).Key} = @objPKValue;";

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                object[] inside = new object[reader.FieldCount];
                reader.GetValues(inside);
                entity = SetValuesByReflection(inside);
            }

            reader.Close();
            command.Dispose();
            conn.Close();

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            ICollection<T> entitys = new List<T>();

            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = $"select {_objPropertiesNames} from {_objName}";

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                object[] objValues = new object[reader.FieldCount];
                reader.GetValues(objValues);
                entitys.Add(SetValuesByReflection(objValues));
            }

            reader.Close();
            command.Dispose();
            conn.Close();

            return entitys;
        }

        // without id
        public void Remove(T obj)
        {
            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            foreach (var item in _objPropertiesInfo.Skip(1))
            {
                sb.Append(split);
                split = " and ";
                sb.Append(item.Key);
                sb.Append("=@");
                sb.Append(item.Key);
            }

            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = $"delete from {_objName} where {sb};";

            for (int i = 1; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue('@' + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            conn.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
        }

        public int Remove(int id)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.Add("@objPKValue", SqlDbType.Int).Value = id;

            command.CommandText = $"delete from {_objName} where {_objPropertiesInfo.ElementAt(0).Key} = @objPKValue;";

            conn.Open();
            var rowAffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return rowAffected;
        }

        // with id
        public void Update(T obj)
        {
            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            foreach (var item in _objPropertiesInfo.Skip(1))
            {
                sb.Append(split);
                split = ",";
                sb.Append(item.Key);
                sb.Append("=@");
                sb.Append(item.Key);
            }

            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            for (int i = 1; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue("@" + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            command.Parameters.AddWithValue("@objPKValue", values.ElementAt(0));

            command.CommandText = $"update {_objName} set {sb} where {_objPropertiesInfo.ElementAt(0).Key} = @objPKValue;";

            conn.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
        }

        // from list values create instanse T
        protected static T SetValuesByReflection(object[] obj)
        {
            T t = new T();

            Type type = typeof(T);
            var properties = type.GetProperties();

            int i = 0;

            if (obj != null)
            {
                foreach (PropertyInfo info in properties)
                {
                    if (obj[i] == DBNull.Value)
                    {
                        obj[i] = null;
                    }

                    info.SetValue(t, obj[i]);
                    i++;
                }
            }

            return t;
        }

        protected static T SetValuesByReflection(T obj, int id)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            properties[0].SetValue(obj, id);

            return obj;
        }

        // get info about properties types and names from main type
        protected static IDictionary<string, string> GetPropertiesNamesAndTypes()
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

        // by instance
        protected IQueryable<object> GetPropertiesValues(T obj)
        {
            ICollection<object> values = new List<object>();
            Type type = typeof(T);

            var entrails = type.GetProperties();

            foreach (PropertyInfo info in entrails)
            {
                values.Add(info.GetValue(obj));
            }

            return values.AsQueryable();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> p)
        {
            throw new NotImplementedException();
        }
    }
}
