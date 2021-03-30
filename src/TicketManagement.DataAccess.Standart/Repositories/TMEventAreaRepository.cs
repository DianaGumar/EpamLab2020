using System.Data.SqlClient;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventAreaRepository : Repository<TMEventArea>, ITMEventAreaRepository
    {
        public TMEventAreaRepository(string conn)
            : base(conn)
        {
        }

        public new TMEventArea Create(TMEventArea obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "insert into TMEventArea (Description, TMEventId, CoordX, CoordY, Price) OUTPUT INSERTED.ID " +
                "values(@Description, @TMEventId, @CoordX, @CoordY, @Price);";

            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200).Value = obj?.Description;
            command.Parameters.Add("@TMEventId", System.Data.SqlDbType.Int).Value = obj?.TMEventId;
            command.Parameters.Add("@CoordX", System.Data.SqlDbType.Int).Value = obj?.CoordX;
            command.Parameters.Add("@CoordY", System.Data.SqlDbType.Int).Value = obj?.CoordY;
            command.Parameters.Add("@Price", System.Data.SqlDbType.Decimal).Value = obj?.Price;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
