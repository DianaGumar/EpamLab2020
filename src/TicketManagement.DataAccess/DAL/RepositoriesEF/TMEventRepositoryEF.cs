using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.DAL
{
    public class TMEventRepositoryEF : RepositoryEF<TMEvent>, ITMEventRepository
    {
        public TMEventRepositoryEF(DbContext conn)
            : base(conn)
        {
        }

        public new TMEvent Create(TMEvent obj)
        {
            SqlParameter name = new SqlParameter("@Name", obj?.Name);
            SqlParameter description = new SqlParameter("@Description", obj?.Description);
            SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", obj?.TMLayoutId);
            SqlParameter startEvent = new SqlParameter("@StartEvent", obj?.StartEvent);
            SqlParameter endEvent = new SqlParameter("@EndEvent", obj?.EndEvent);
            SqlParameter img = new SqlParameter("@Img", obj?.Img);

            obj = DbContext.Database.SqlQuery<TMEvent>("SP_Create_TMEvent",
                name, description, tmlayoutId, startEvent, endEvent, img).SingleOrDefault();

            return obj;
        }

        public new int Remove(int id)
        {
            SqlParameter idParameter = new SqlParameter("@TMEventId", id);

            DbContext.Database.SqlQuery<TMEvent>("SP_Delete_TMEvent", idParameter);

            return 1;
        }

        public new int Update(TMEvent obj)
        {
            SqlParameter name = new SqlParameter("@Name", obj?.Name);
            SqlParameter description = new SqlParameter("@Description", obj?.Description);
            SqlParameter tmlayoutId = new SqlParameter("@TMLayoutId", obj?.TMLayoutId);
            SqlParameter startEvent = new SqlParameter("@StartEvent", obj?.StartEvent);
            SqlParameter endEvent = new SqlParameter("@EndEvent", obj?.EndEvent);
            SqlParameter img = new SqlParameter("@Img", obj?.Img);

            DbContext.Database.SqlQuery<TMEvent>("SP_Update_TMEvent",
                name, description, tmlayoutId, startEvent, endEvent, img);

            return 1;
        }
    }
}
