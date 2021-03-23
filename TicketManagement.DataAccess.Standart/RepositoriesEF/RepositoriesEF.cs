using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TopTMEventId
    {
        public int TMEventId { get; set; }
    }

    public class CountRowAffected
    {
        public int CountRow { get; set; }
    }

    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(TMContext conn)
            : base(conn)
        {
            Context = conn;
        }

        protected new TMContext Context { get; }

        public new TMEvent Create(TMEvent obj)
        {
            SqlParameter name = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 120);
            SqlParameter description = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, -1);
            SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", System.Data.SqlDbType.Int);
            SqlParameter startEvent = new SqlParameter("@StartEvent", System.Data.SqlDbType.DateTime);
            SqlParameter endEvent = new SqlParameter("@EndEvent", System.Data.SqlDbType.DateTime);
            SqlParameter img = new SqlParameter("@Img", System.Data.SqlDbType.NVarChar);

            name.Value = obj?.Name;
            description.Value = obj?.Description;
            tmlayoutId.Value = obj?.TMLayoutId;
            startEvent.Value = obj?.StartEvent;
            endEvent.Value = obj?.EndEvent;
            img.Value = obj?.Img;

            var result = Context.Set<TopTMEventId>()
                .FromSqlRaw("EXEC SP_Create_TMEvent @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
                name, description, tmlayoutId, startEvent, endEvent, img).ToList();

            if (obj != null)
            {
                obj.Id = result[0].TMEventId;
            }

            return obj;
        }

        public new int Remove(int id)
        {
            SqlParameter idParam = new SqlParameter("@TMEventId", System.Data.SqlDbType.Int);
            idParam.Value = id;

            var result = Context.Set<CountRowAffected>()
                .FromSqlRaw("EXEC SP_Delete_TMEvent @TMEventId", idParam).ToList();

            return result[0].CountRow;
        }

        public new int Update(TMEvent obj)
        {
            SqlParameter name = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 120);
            SqlParameter description = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, -1);
            SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", System.Data.SqlDbType.Int);
            SqlParameter startEvent = new SqlParameter("@StartEvent", System.Data.SqlDbType.DateTime);
            SqlParameter endEvent = new SqlParameter("@EndEvent", System.Data.SqlDbType.DateTime);
            SqlParameter img = new SqlParameter("@Img", System.Data.SqlDbType.NVarChar);

            name.Value = obj?.Name;
            description.Value = obj?.Description;
            tmlayoutId.Value = obj?.TMLayoutId;
            startEvent.Value = obj?.StartEvent;
            endEvent.Value = obj?.EndEvent;
            img.Value = obj?.Img;

            var result = Context.Set<CountRowAffected>()
                .FromSqlRaw("EXEC SP_Update_TMEvent @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
                name, description, tmlayoutId, startEvent, endEvent, img).ToList();

            return result[0].CountRow;
        }
    }

    public class AreaRepositoryEF : RepositoryEF<Area>, IAreaRepository
    {
        public AreaRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class PurchaseHistoryRepositoryEF : RepositoryEF<PurchaseHistory>, IPurchaseHistoryRepository
    {
        public PurchaseHistoryRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class SeatRepositoryEF : RepositoryEF<Seat>, ISeatRepository
    {
        public SeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMEventAreaRepositoryEF : RepositoryEF<TMEventArea>, ITMEventAreaRepository
    {
        public TMEventAreaRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMEventSeatRepositoryEF : RepositoryEF<TMEventSeat>, ITMEventSeatRepository
    {
        public TMEventSeatRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMLayoutRepositoryEF : RepositoryEF<TMLayout>, ITMLayoutRepository
    {
        public TMLayoutRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class TMUserRepositoryEF : RepositoryEF<TMUser>, ITMUserRepository
    {
        public TMUserRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }

    public class VenueRepositoryEF : RepositoryEF<Venue>, IVenueRepository
    {
        public VenueRepositoryEF(TMContext conn)
            : base(conn)
        {
        }
    }
}
