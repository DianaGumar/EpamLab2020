////using System;
////using System.Data.Entity;
////using System.Data.SqlClient;
////////using System.Diagnostics;
////using System.Linq;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(TMContext conn)
            : base(conn)
        {
        }

        ////public new TMEvent Create(TMEvent obj)
        ////{
        ////    SqlParameter name = new SqlParameter("@Name", obj?.Name);
        ////    SqlParameter description = new SqlParameter("@Description", obj?.Description);
        ////    SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", obj?.TMLayoutId);
        ////    SqlParameter startEvent = new SqlParameter("@StartEvent", obj?.StartEvent);
        ////    SqlParameter endEvent = new SqlParameter("@EndEvent", obj?.EndEvent);
        ////    SqlParameter img = new SqlParameter("@Img", obj?.Img ?? (object)DBNull.Value);

        ////    int id = Context.Database.SqlQuery<int>(
        ////        "SP_Create_TMEvent @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
        ////        name, description, tmlayoutId, startEvent, endEvent, img).SingleOrDefault();

        ////    Context.SaveChanges();

        ////    return GetById(id);
        ////}

        ////public new void Remove(int id)
        ////{
        ////    SqlParameter tmeventId = new SqlParameter("@Id", id);

        ////    TMEvent te = GetById(id);

        ////    Context.Database.ExecuteSqlCommand("TMEvent_Delete @Id", tmeventId);

        ////    Context.Entry(te).State = EntityState.Deleted;

        ////    Context.SaveChanges();

        ////    Context.Database.Log = (e) => { Debug.WriteLine(e); };
        ////}

        ////public new void Update(TMEvent obj)
        ////{
        ////    SqlParameter id = new SqlParameter("@TMEventId", obj?.Id);
        ////    SqlParameter name = new SqlParameter("@Name", obj?.Name);
        ////    SqlParameter description = new SqlParameter("@Description", obj?.Description);
        ////    SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", obj?.TMLayoutId);
        ////    SqlParameter startEvent = new SqlParameter("@StartEvent", obj?.StartEvent);
        ////    SqlParameter endEvent = new SqlParameter("@EndEvent", obj?.EndEvent);
        ////    SqlParameter img = new SqlParameter("@Img", obj?.Img ?? (object)DBNull.Value);

        ////    Context.Database.ExecuteSqlCommand(
        ////        "SP_Update_TMEvent @TMEventId, @Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img",
        ////        id, name, description, tmlayoutId, startEvent, endEvent, img);

        ////    Context.SaveChanges();
        ////}
    }
}
