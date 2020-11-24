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
            builder.RegisterType<TMContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RepositoryEF<>)).As(typeof(IRepository<>));

            builder.RegisterType<AreaRepositoryEF>().As<IAreaRepository>();
            builder.RegisterType<TMUserRepositoryEF>().As<ITMUserRepository>();
            builder.RegisterType<PurchaseHistoryRepositoryEF>().As<IPurchaseHistoryRepository>();
            builder.RegisterType<SeatRepositoryEF>().As<ISeatRepository>();
            builder.RegisterType<TMEventAreaRepositoryEF>().As<ITMEventAreaRepository>();
            builder.RegisterType<TMEventSeatRepositoryEF>().As<ITMEventSeatRepository>();
            builder.RegisterType<TMEventRepositoryEF>().As<ITMEventRepository>();
            builder.RegisterType<VenueRepositoryEF>().As<IVenueRepository>();
            builder.RegisterType<TMLayoutRepositoryEF>().As<ITMLayoutRepository>();

            ////using (var context = new TMContext())
            ////{
            ////    context.Database.Initialize(true);
            ////}
        }
    }
}