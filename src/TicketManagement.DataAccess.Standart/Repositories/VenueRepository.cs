using System;
using System.Data.SqlClient;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

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
            command.CommandText = "insert into Venue (Description, Address, Weidth, Lenght, Phone) OUTPUT INSERTED.ID " +
                "values(@Description, @Address, @Weidth, @Lenght, @Phone);";

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
                    Value = (object)obj?.Phone ?? DBNull.Value,
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
