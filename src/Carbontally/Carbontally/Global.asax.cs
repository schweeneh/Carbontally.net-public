using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Carbontally.Domain.Persistence;
using Carbontally.Infrastructure;
using WebMatrix.WebData;
using log4net;

namespace Carbontally
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger("Global.asax");

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Info("Application starting...");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarbontallyContext>());
            Initializer.InitializeSimpleMemberShip();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}