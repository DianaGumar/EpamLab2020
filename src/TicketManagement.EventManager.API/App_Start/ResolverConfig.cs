using System.Collections.Generic;
using Autofac;

namespace TicketManagement.Web
{
    public static class ResolverConfig
    {
        /// <summary>
        /// The register resolver.
        /// </summary>
        public static void RegisterResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterInstance(RouteTable.Routes).As<IEnumerable<RouteBase>>();
            ////builder.RegisterModule<DbServiceModule>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}