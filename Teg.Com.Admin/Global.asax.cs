using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Teg.Com.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IDisposable container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected virtual void Application_EndRequest()
        {
            var entityContext = HttpContext.Current.Items["BookingDbContext"] as IDisposable;
            if (entityContext != null)
                entityContext.Dispose();
        }

        protected virtual void Application_End()
        {
            container.Dispose();
        }
    }
}
