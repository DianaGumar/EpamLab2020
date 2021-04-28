////using System.Data.SqlClient;
using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.DAL;
using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.DataAccess.DAL
{
    // сомнительные классы for ef core stored prosedures calling
    public class TopTMEventId
    {
        public int Id { get; set; }
    }

    public class CountRow
    {
        public int CountRowAffected { get; set; }
    }

    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(TMContext conn)
            : base(conn)
        {
        }

#pragma warning disable S1541 // Methods and properties should not be too complex
        public new TMEvent Create(TMEvent obj)
#pragma warning restore S1541 // Methods and properties should not be too complex
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

            if (obj?.Img == null)
            {
                img.Value = DBNull.Value;
            }
            else
            {
                img.Value = obj?.Img;
            }

            var result = Context.Set<TopTMEventId>()
                .FromSqlRaw("EXEC TMEvent_Create @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
                name, description, tmlayoutId, startEvent, endEvent, img).ToList();

            if (obj != null)
            {
                obj.Id = result[0].Id;
            }

            Context.ChangeTracker.Clear();
            ////foreach (var entity in Context.ChangeTracker.Entries())
            ////{
            ////    /////entity.Reload();
            ////    entity.State = EntityState.Detached;
            ////}

            return obj;
        }

        public new int Remove(int id)
        {
            if (Context.Set<TMEvent>().Find(id) == null)
            {
                return 0;
            }

            SqlParameter idParam = new SqlParameter("@TMEventId", System.Data.SqlDbType.Int);
            idParam.Value = id;

            var result = Context.Set<CountRow>()
                .FromSqlRaw("EXEC TMEvent_Delete @TMEventId", idParam).ToList();

            Context.ChangeTracker.Clear();
            ////foreach (var entity in Context.ChangeTracker.Entries())
            ////{
            ////    ////entity.Reload();
            ////    entity.State = EntityState.Detached;
            ////}

            return result[0].CountRowAffected;
        }

#pragma warning disable S1541 // Methods and properties should not be too complex
        public new int Update(TMEvent obj)
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            if (Context.Set<TMEvent>().Find(obj) == null)
            {
                return 0;
            }

            SqlParameter id = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            SqlParameter name = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 120);
            SqlParameter description = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, -1);
            SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", System.Data.SqlDbType.Int);
            SqlParameter startEvent = new SqlParameter("@StartEvent", System.Data.SqlDbType.DateTime);
            SqlParameter endEvent = new SqlParameter("@EndEvent", System.Data.SqlDbType.DateTime);
            SqlParameter img = new SqlParameter("@Img", System.Data.SqlDbType.NVarChar);

            id.Value = obj?.Id;
            name.Value = obj?.Name;
            description.Value = obj?.Description;
            tmlayoutId.Value = obj?.TMLayoutId;
            startEvent.Value = obj?.StartEvent;
            endEvent.Value = obj?.EndEvent;

            if (obj?.Img == null)
            {
                img.Value = DBNull.Value;
            }
            else
            {
                img.Value = obj?.Img;
            }

            var result = Context.Set<CountRow>()
                .FromSqlRaw("EXEC TMEvent_Update @Id, @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
                id, name, description, tmlayoutId, startEvent, endEvent, img).ToList();

            Context.ChangeTracker.Clear();
            ////foreach (var entity in Context.ChangeTracker.Entries())
            ////{
            ////    ////entity.Reload();
            ////    entity.State = EntityState.Detached;
            ////}

            return result[0].CountRowAffected;
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
