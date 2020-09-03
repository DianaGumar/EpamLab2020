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
        //private readonly SqlConnection _conn;

        //// inflexible with db changing
        //public Repository(SqlConnection conn)
        //{
        //    _conn = conn;
        //}

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

            ////create sql query
            //List<object> values = GetEntrailsValues(obj);

            //StringBuilder sb = new StringBuilder();
            //sb.Append("insert into " + Name + "s (");
            //int count = Entrails[0].Count;
            //for (int i = 1; i < count - 1; i++)
            //{
            //    sb.Append(Entrails[1][i] + ",");
            //}
            //sb.Append(Entrails[1][count - 1]);
            //sb.Append(") values( ");
            //for (int i = 1; i < count - 1; i++)
            //{
            //    if (Entrails[0][i].Equals("DateTime"))
            //    {
            //        values[i] = ((DateTime)values[i]).ToString("yyyy-MM-dd");
            //    }
            //    sb.Append("'" + values[i] + "', ");
            //}
            //if (Entrails[0][count - 1].Equals("DateTime"))
            //{
            //    values[count - 1] = ((DateTime)values[count - 1]).ToString("yyyy-MM-dd");
            //}
            //sb.Append(values[count - 1] + " )");

            //string sql = sb.ToString();
            ////            insert into comission.entrants(EntrantName, ScoreDiploma, Student)
            ////            values('Mik', '5', 0);
            //int countRowsUffected = 0;

            ////----------------------------------

            //using(SqlConnection conn = new SqlConnection(_strConn))
            //{
            //    conn.Open(); //?

            //    SqlCommand command = new SqlCommand(sql, conn);
            //    countRowsUffected = command.ExecuteNonQuery();
            //}

            //return countRowsUffected;

            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            T entity = new T();

            string sql = "select " + objPropertiesNames
                + " from " + objName
                + " where " + objPropertiesInfo.ElementAt(0).Key + "=" + id;


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
            ICollection<T> entitys = new List<T>();

            string sql = "select " + objPropertiesNames 
                + " from " + objName;


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
            for (int i = 0; i < values.Count() - 1; i++)
            {
                sb.Append(objPropertiesInfo.ElementAt(i).Key);
                sb.Append("=");
                sb.Append(values.ElementAt(i));
                sb.Append(",");
            }
            sb.Append(objPropertiesInfo.ElementAt(values.Count() - 1).Key);
            sb.Append("=");
            sb.Append(values.ElementAt(values.Count() - 1));


            string sql = "delete * from " + objName + " where " + sb.ToString();

            int countRowsUffected = 0;

            using(SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                countRowsUffected = command.ExecuteNonQuery();
            }
               
            return countRowsUffected;
        }

        public int Remove(int id)
        {
            string sql = "delete * from " + objName 
                + " where " + objPropertiesInfo.ElementAt(0).Key + "=" + id;

            int countRowsUffected = 0;

            using (SqlConnection conn = new SqlConnection(_strConn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                countRowsUffected = command.ExecuteNonQuery();
            }

            return countRowsUffected;
        }

        public int Update(T obj)
        {

            ////create sql query
            //List<object> values = GetEntrailsValues(obj);

            //StringBuilder sb = new StringBuilder();
            //sb.Append("update " + Name + "s set ");
            //int count = Entrails[0].Count;
            //for (int i = 1; i < count - 1; i++)
            //{
            //    //проверка на злощастный тип даты- из за несовпдения форматов
            //    if (Entrails[0][i].Equals("DateTime"))
            //    {
            //        values[i] = ((DateTime)values[i]).ToString("yyyy-MM-dd");
            //    }
            //    sb.Append(Entrails[1][i] + "= '" + values[i] + "', ");
            //}
            //if (Entrails[0][count - 1].Equals("DateTime"))
            //{
            //    values[count - 1] = ((DateTime)values[count - 1]).ToString("yyyy-MM-dd");
            //}
            //sb.Append(Entrails[1][count - 1] + "= '" + values[count - 1] + "'");
            //sb.Append(" where " + Entrails[1][0] + "= " + values[0]);

            //string sql = sb.ToString();
            //// update comission.entrants set EntrantName = 'Igor', ScoreDiploma = 8, Student = 0 
            ////where EntrantID = 6;
            //int countRowsUffected = 0;

            ////----------------------------------

            //try
            //{
            //    connection.Open();
            //    MySqlCommand command = new MySqlCommand(sql, connection);
            //    countRowsUffected = command.ExecuteNonQuery();

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Connect to bd exeption: ", e.Message);
            //}
            //finally { connection.Close(); }

            //return countRowsUffected;

            throw new NotImplementedException();
        }


        // todo ? get out into new class - only reflection methods

        // for reflectoin work
        private string objName = typeof(T).Name;
        private IDictionary<string, string> objPropertiesInfo;
        string objPropertiesNames;

        // some methods for beter work with reflection

        // from list values create full objet
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
