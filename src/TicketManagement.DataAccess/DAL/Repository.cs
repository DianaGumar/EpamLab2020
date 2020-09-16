using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TicketManagement.DataAccess.DAL
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly string _strConn;

        // for reflectoin work
        private readonly string _objPropertiesNames;

        private readonly string _objName = typeof(T).Name;

        private readonly IDictionary<string, string> _objPropertiesInfo;

        public Repository(string conn)
        {
            _strConn = conn;

            _objPropertiesInfo = GetPropertiesNamesAndTypes();
            _objPropertiesNames = string.Join(",", _objPropertiesInfo.Keys.ToArray());
        }

        // some ADO code methods (with reflection)
        public int Create(T obj)
        {
            // will be care about null checking
            IEnumerable<object> values = GetPropertiesValues(obj);

            ICollection<string> list = new List<string>();
            for (int i = 1; i < _objPropertiesInfo.Count; i++)
            {
                list.Add(_objPropertiesInfo.Keys.ElementAt(i));
            }

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

            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            for (int i = 1; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue('@' + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;

            command.Parameters.AddWithValue("@objPropertiesNamesWithoutId", objPropertiesNamesWithoutId);
            command.Parameters.AddWithValue("@objPropertiesInfo", sb.ToString());

            command.CommandText = "insert into @objName(@objPropertiesNamesWithoutId) values(@objPropertiesInfo);";

            conn.Open();
            int countRowsUffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowsUffected;
        }

        public T GetById(int id)
        {
            T entity = new T();

            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.Add("@objPKValue", SqlDbType.Int).Value = id;
            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;
            command.Parameters.Add("@objPKName", SqlDbType.NChar).Value = _objPropertiesInfo.ElementAt(0).Key;
            command.Parameters.AddWithValue("@objPropertiesNames", _objPropertiesNames);

            command.CommandText = "select @objPropertiesNames from @objName where @objPKName = @objPKValue;";

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

            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.AddWithValue("@objPropertiesNames", _objPropertiesNames);
            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;

            command.CommandText = "select @objPropertiesNames from @objName";

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

        public int Remove(T obj)
        {
            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            foreach (var item in _objPropertiesInfo)
            {
                sb.Append(split);
                split = " and ";
                sb.Append(item.Key);
                sb.Append("=@");
                sb.Append(item.Key);
            }

            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;
            command.Parameters.AddWithValue("@objPropertiesInfo", sb.ToString());

            command.CommandText = "delete from @objName where @objPropertiesInfo;";

            for (int i = 0; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue('@' + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            conn.Open();
            int countRowsUffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowsUffected;
        }

        public int Remove(int id)
        {
            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.Parameters.Add("@objPKValue", SqlDbType.Int).Value = id;
            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;
            command.Parameters.Add("@objPKName", SqlDbType.NChar).Value = _objPropertiesInfo.ElementAt(0).Key;

            command.CommandText = "delete from @objName where @objPKName = @objPKValue;";

            conn.Open();
            int countRowsUffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowsUffected;
        }

        public int Update(T obj)
        {
            // not by id
            // but by all fields
            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            for (int i = 1; i < values.Count(); i++)
            {
                sb.Append(split);
                split = ",";
                sb.Append(_objPropertiesInfo.ElementAt(i).Key);
                sb.Append("=@");
                sb.Append(_objPropertiesInfo.ElementAt(i).Key);
            }

            SqlConnection conn = new SqlConnection(_strConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            for (int i = 1; i < values.Count(); i++)
            {
                command.Parameters.AddWithValue("@" + _objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
            }

            command.Parameters.Add("@objName", SqlDbType.NChar).Value = _objName;
            command.Parameters.AddWithValue("@objPropertiesInfo", sb.ToString());
            command.Parameters.Add("@objPKName", SqlDbType.NChar).Value = _objPropertiesInfo.ElementAt(0).Key;
            command.Parameters.AddWithValue("@objPKValue", _objPropertiesInfo.ElementAt(0).Value);

            command.CommandText = "update @objName set @objPropertiesInfo where @objPKName = @objPKValue;";

            conn.Open();
            int countRowsUffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowsUffected;
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
                    info.SetValue(t, obj[i]);
                    i++;
                }
            }

            return t;
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
        protected IEnumerable<object> GetPropertiesValues(T obj)
        {
            ICollection<object> values = new List<object>();
            Type type = typeof(T);

            var entrails = type.GetProperties();

            foreach (PropertyInfo info in entrails)
            {
                values.Add(info.GetValue(obj));
            }

            return values;
        }
    }
}
