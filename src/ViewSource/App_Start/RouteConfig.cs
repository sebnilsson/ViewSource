using System.Web.Mvc;
using System.Web.Routing;

namespace ViewSource
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapSeoRoute(
                name: "Result",
                url: "result",
                defaults: new { controller = "Home", action = "Result", id = UrlParameter.Optional });

            routes.MapSeoRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //routes.MapSeoRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}