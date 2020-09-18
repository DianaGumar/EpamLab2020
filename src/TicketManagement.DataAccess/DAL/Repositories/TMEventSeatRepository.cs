using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventSeatRepository : Repository<TMEventSeat>, ITMEventSeatRepository
    {
        public TMEventSeatRepository(string conn)
            : base(conn)
        {
        }

        public new TMEventSeat Create(TMEventSeat obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "insert into TMEventSeat (TMEventAreaId, Number, Row, State) OUTPUT INSERTED.ID " +
                "values(@TMEventAreaId, @Number, @Row, @State);";

            command.Parameters.Add("@TMEventAreaId", System.Data.SqlDbType.Int).Value = obj?.TMEventAreaId;
            command.Parameters.Add("@Number", System.Data.SqlDbType.Int).Value = obj?.Number;
            command.Parameters.Add("@Row", System.Data.SqlDbType.Int).Value = obj?.Row;
            command.Parameters.Add("@State", System.Data.SqlDbType.Int).Value = obj?.State;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
