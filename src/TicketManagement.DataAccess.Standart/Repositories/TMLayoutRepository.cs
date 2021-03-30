using System.Data.SqlClient;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMLayoutRepository : Repository<TMLayout>, ITMLayoutRepository
    {
        public TMLayoutRepository(string conn)
            : base(conn)
        {
        }

        public new TMLayout Create(TMLayout obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "insert into TMLayout (Description, VenueId) OUTPUT INSERTED.ID " +
                "values(@Description, @VenueId);";

            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Description;
            command.Parameters.Add("@VenueId", System.Data.SqlDbType.Int).Value = obj?.VenueId;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
