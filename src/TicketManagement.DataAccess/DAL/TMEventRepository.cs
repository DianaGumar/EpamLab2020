using System;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class TMEventRepository : Repository<TMEvent>, ITMEventRepository
    {
        internal TMEventRepository(string conn)
            : base(conn)
        {
        }

        internal new int Create(TMEvent obj)
        {
            return Create(obj, 0);
        }

        internal int Create(TMEvent obj, decimal arwaPrice)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Create_PublicEvent";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Name;
            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, -1).Value = obj?.Description;
            command.Parameters.Add("@LayoutId", System.Data.SqlDbType.Int).Value = obj?.TMLayoutId;
            command.Parameters.Add("@StartEvent", System.Data.SqlDbType.DateTime).Value = obj?.StartEvent;
            command.Parameters.Add("@EndEvent", System.Data.SqlDbType.DateTime).Value = obj?.EndEvent;
            command.Parameters.Add("@Price", System.Data.SqlDbType.Money).Value = arwaPrice;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            return idNewObj != null ? (int)idNewObj : 0;
        }

        internal new int Remove(TMEvent obj)
        {
            throw new NotImplementedException();
        }

        internal new int Remove(int id)
        {
            throw new NotImplementedException();
        }

        internal new int Update(TMEvent obj)
        {
            throw new NotImplementedException();
        }
    }
}
