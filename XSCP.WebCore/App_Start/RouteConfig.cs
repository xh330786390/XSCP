using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XSCP.WebCore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Xscp", action = "HightCharts", id = UrlParameter.Optional }
                //defaults: new { controller = "Xscp", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Xscp", action = "Main", id = UrlParameter.Optional }
                defaults: new { controller = "Tendency", action = "Main", id = UrlParameter.Optional }
            );
        }
    }
}
