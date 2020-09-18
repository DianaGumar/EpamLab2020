using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public class VenueRepository : Repository<Venue>, IVenueRepository
    {
        public VenueRepository(string conn)
            : base(conn)
        {
        }

        public new Venue Create(Venue obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Create_Venue";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Description;
            command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar, 200).Value = obj?.Address;
            command.Parameters.Add("@Weidth", System.Data.SqlDbType.Int).Value = obj?.Weidth;
            command.Parameters.Add("@Lenght", System.Data.SqlDbType.Int).Value = obj?.Lenght;
            command.Parameters.Add(
                new SqlParameter
                {
                    ParameterName = "@Phone",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 30,
                    IsNullable = true,
                    Value = obj?.Phone,
                });

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            SetValuesByReflection(obj, idNewObj != null ? (int)idNewObj : 0);
            return obj;
        }
    }
}
