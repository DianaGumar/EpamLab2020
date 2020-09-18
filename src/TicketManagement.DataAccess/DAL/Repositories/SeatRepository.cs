using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(string conn)
            : base(conn)
        {
        }

        public new Seat Create(Seat obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "insert into Seat (AreaId, Number, Row) " +
                "values(@AreaId, @Number, @Row);";

            command.Parameters.Add("@AreaId", System.Data.SqlDbType.Int).Value = obj?.AreaId;
            command.Parameters.Add("@Number", System.Data.SqlDbType.Int).Value = obj?.Number;
            command.Parameters.Add("@Row", System.Data.SqlDbType.Int).Value = obj?.Row;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
