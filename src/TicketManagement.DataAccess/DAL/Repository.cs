using System;
using System.Collections.Generic;
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

        public Repository(string conn)
        {
            _strConn = conn;

            objPropertiesInfo = GetPropertiesTypesAndNames();
            objPropertiesNames = string.Join(",", objPropertiesInfo.Keys.ToArray());
        }


        // some ADO code methods (with reflection)

        public int Create(T obj)
        {
 
            // will be care about null checking

            //if (objPropertiesInfo.ElementAt(i).Key.Equals("DateTime")
            //       && values[i].ToString().Equals(DateTime.MinValue.ToString()))
            //{
            //    values[i] = null;
            //}

            //if (values[i] != null)
            //{
            //    if (values[i].ToString() != "")
            //    {
            //        sb.Append(prefix);
            //        prefix = ",";
            //        sb.Append(objPropertiesInfo.ElementAt(i).Key);
            //    }
            //}


            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();

            string split = "";
            foreach (var item in objPropertiesInfo)
            {
                sb.Append(split);
                split = ",";
                sb.Append("@");
                sb.Append(item.Key);
            }

            string sql = String.Format("insert into {0}({1}) values({2});",
               objName, objPropertiesNames, sb.ToString());

            int countRowsUffected = 0;
            using(SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                for (int i = 0; i < values.Count(); i++)
                {
                    command.Parameters.AddWithValue("@" + objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
                }

                countRowsUffected = command.ExecuteNonQuery();
            }

            return countRowsUffected;
        }

        public T GetById(int id)
        {
            T entity = new T();

            string sql = String.Format("select {0} from {1} where {2} = @value;",
               objPropertiesNames, objName, objPropertiesInfo.ElementAt(0).Key);

            // from pull conections in future
            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlParameter param = new SqlParameter("@value", id);
                command.Parameters.Add(param);

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
            ICollection<T> entitys = new List<T>();

            string sql = String.Format("select {0} from {1}",
               objPropertiesNames, objName);

            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object[] objValues = new object[reader.FieldCount];
                    reader.GetValues(objValues);
                    entitys.Add(SetValuesByReflection(objValues));
                }

                reader.Close();

            }

            return entitys;
        }

        public int Remove(T obj)
        {
            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            foreach (var item in objPropertiesInfo)
            {
                sb.Append(split);
                split = ",";
                sb.Append(item.Key);
                sb.Append("=@");
                sb.Append(item.Key);
            }

            string sql = String.Format("delete * from {0} where {1};", objName, sb.ToString());

            int countRowsUffected = 0;

            using(SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                for (int i = 0; i < values.Count(); i++)
                {
                    command.Parameters.AddWithValue("@" + objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
                }

                countRowsUffected = command.ExecuteNonQuery();
            }
               
            return countRowsUffected;
        }

        public int Remove(int id)
        {
            string sql = String.Format("delete * from {0} where {1} = @value;", 
                objName, objPropertiesInfo.ElementAt(0).Key);

            int countRowsUffected = 0;

            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlParameter param = new SqlParameter("@value", id);
                command.Parameters.Add(param);

                countRowsUffected = command.ExecuteNonQuery();
            }

            return countRowsUffected;
        }

        public int Update(T obj)
        {

            //UPDATE table_name
            //SET column1 = value1, column2 = value2, ...
            //WHERE condition;

            IEnumerable<object> values = GetPropertiesValues(obj);

            StringBuilder sb = new StringBuilder();
            string split = "";

            for (int i = 1; i < values.Count(); i++)
            {
                sb.Append(split);
                split = ",";
                sb.Append(objPropertiesInfo.ElementAt(i).Key);
                sb.Append("=@");
                sb.Append(objPropertiesInfo.ElementAt(i).Key);
            }

            string sql = String.Format("update {0} set {1} where {2} = @{2};", 
                objName, sb.ToString(), objPropertiesInfo.ElementAt(0).Key);

            int countRowsUffected = 0;

            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                for (int i = 0; i < values.Count(); i++)
                {
                    command.Parameters.AddWithValue("@" + objPropertiesInfo.ElementAt(i).Key, values.ElementAt(i));
                }

                countRowsUffected = command.ExecuteNonQuery();
            }

            return countRowsUffected;
        }



        // for reflectoin work
        private string objName = typeof(T).Name;
        private IDictionary<string, string> objPropertiesInfo;
        private string objPropertiesNames;

        // from list values create instanse T
        protected static T SetValuesByReflection(object[] obj)
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
        protected static IDictionary<string, string> GetPropertiesTypesAndNames()
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

            var Entrails = type.GetProperties();

            foreach (PropertyInfo info in Entrails)
            {
                values.Add(info.GetValue(obj));
            }

            return values;
        }

    }
}
