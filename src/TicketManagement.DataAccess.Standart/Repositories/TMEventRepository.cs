using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventRepository : Repository<TMEvent>, ITMEventRepository
    {
        public TMEventRepository(string conn)
            : base(conn)
        {
        }

        public new TMEvent Create(TMEvent obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Create_TMEvent";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Name;
            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, -1).Value = obj?.Description;
            command.Parameters.Add("@TMLayoutId", System.Data.SqlDbType.Int).Value = obj?.TMLayoutId;
            command.Parameters.Add("@StartEvent", System.Data.SqlDbType.DateTime).Value = obj?.StartEvent;
            command.Parameters.Add("@EndEvent", System.Data.SqlDbType.DateTime).Value = obj?.EndEvent;
            command.Parameters.Add("@Img", System.Data.SqlDbType.NVarChar).Value = obj?.Img;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();
            var t = idNewObj.GetType();
            t.ToString();
            SetValuesByReflection(obj, (int)idNewObj);
            return obj;
        }

        public bool IsValid(TMEvent obj, List<TMEvent> objs, out string strErrors)
        {
            throw new NotImplementedException();
        }

        // without id
        public new int Remove(TMEvent obj)
        {
            throw new NotImplementedException();
        }

        public new int Remove(int id)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Delete_TMEvent";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@TMEventId", System.Data.SqlDbType.Int).Value = id;

            conn.Open();
            var countRowAffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowAffected;
        }

        public new int Update(TMEvent obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Update_TMEvent";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@TMEventId", System.Data.SqlDbType.Int).Value = obj?.Id;
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Name;
            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, -1).Value = obj?.Description;
            command.Parameters.Add("@LayoutId", System.Data.SqlDbType.Int).Value = obj?.TMLayoutId;
            command.Parameters.Add("@StartEvent", System.Data.SqlDbType.DateTime).Value = obj?.StartEvent;
            command.Parameters.Add("@EndEvent", System.Data.SqlDbType.DateTime).Value = obj?.EndEvent;
            command.Parameters.Add("@Img", System.Data.SqlDbType.NVarChar).Value = obj?.Img;

            conn.Open();
            var countRowAffected = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();

            return countRowAffected;
        }
    }
}
