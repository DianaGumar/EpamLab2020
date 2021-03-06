﻿using Autofac;
using TicketManagement.BusinessLogic;

namespace TicketManagement.Web
{
    public class DbServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<DbRepositoryModule>();
            builder.RegisterType<AreaService>().As<IAreaService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<SeatService>().As<ISeatService>();
            builder.RegisterType<TMEventAreaService>().As<ITMEventAreaService>();
            builder.RegisterType<TMEventSeatService>().As<ITMEventSeatService>();
            builder.RegisterType<TMEventService>().As<ITMEventService>();
            builder.RegisterType<VenueService>().As<IVenueService>();
            builder.RegisterType<TMLayoutService>().As<ITMLayoutService>();
            builder.RegisterType<PurchaceService>().As<IPurchaceService>();
        }
    }
}