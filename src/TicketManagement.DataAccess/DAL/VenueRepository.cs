﻿using System.Data.SqlClient;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    internal class VenueRepository : Repository<Venue>, IVenueRepository
    {
        internal VenueRepository(string conn)
            : base(conn)
        {
        }

        internal new int Create(Venue obj)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SP_Create_Venue";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 120).Value = obj?.Description;
            command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar, 200).Value = obj?.Address;
            command.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar, 30).Value = obj?.Phone;
            command.Parameters.Add("@Weidth", System.Data.SqlDbType.Int).Value = obj?.Weidth;
            command.Parameters.Add("@Lenght", System.Data.SqlDbType.Int).Value = obj?.Lenght;

            conn.Open();
            var idNewObj = command.ExecuteScalar();
            command.Dispose();
            conn.Close();

            return idNewObj != null ? (int)idNewObj : 0;
        }
    }
}
