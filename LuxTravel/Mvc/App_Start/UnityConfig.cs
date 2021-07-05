using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Infrastructure;
using Data.Infrastructure.Interfaces;
using Service.Interfaces;
using Service.Services;
using Unity;
using Unity.Mvc5;

namespace Mvc.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IHotelService, HotelService>();
            container.RegisterType<IRoomService, RoomService>();
            container.RegisterType<IOfferService, OfferService>();

            container.RegisterType<ICommonService, CommonService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}