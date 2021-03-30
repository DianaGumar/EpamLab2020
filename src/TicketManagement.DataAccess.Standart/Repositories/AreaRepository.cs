using System.Data.SqlClient;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(string conn)
            : base(conn)
        {
        }

        public new Area Create(Area obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "insert into Area (Description, TMLayoutId, CoordX, CoordY) OUTPUT INSERTED.ID " +
                "values(@Description, @TMLayoutId, @CoordX, @CoordY);";

            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200).Value = obj?.Description;
            command.Parameters.Add("@TMLayoutId", System.Data.SqlDbType.Int).Value = obj?.TMLayoutId;
            command.Parameters.Add("@CoordX", System.Data.SqlDbType.Int).Value = obj?.CoordX;
            command.Parameters.Add("@CoordY", System.Data.SqlDbType.Int).Value = obj?.CoordY;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
