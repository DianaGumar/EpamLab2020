using Autofac;
using TicketManagement.DataAccess;
using TicketManagement.DataAccess.DAL;

namespace TicketManagement.Web
{
    public class DbRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AreaRepository>().As<IAreaRepository>();
            builder.RegisterType<SeatRepository>().As<ISeatRepository>();
            builder.RegisterType<TMEventAreaRepository>().As<ITMEventAreaRepository>();
            builder.RegisterType<TMEventSeatRepository>().As<ITMEventSeatRepository>();
            builder.RegisterType<TMEventRepository>().As<ITMEventRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();
            builder.RegisterType<TMLayoutRepository>().As<ITMLayoutRepository>();

            builder.RegisterGeneric(typeof(RepositoryEF<>)).As(typeof(IRepository<>));

            using (var context = new TMContext())
            {
                ////context.Database.Initialize(false);
            }
        }
    }
}