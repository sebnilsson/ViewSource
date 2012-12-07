using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViewSource
{
    public static class RouteCollectionExtensions
    {
        public static void MapSeoRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapSeoRoute(name, url, defaults, null);
        }

        public static void MapSeoRoute(
            this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new SeoRoute(url, new MvcRouteHandler())
                            {
                                Defaults = new RouteValueDictionary(defaults),
                                Constraints = new RouteValueDictionary(constraints)
                            };

            if (string.IsNullOrEmpty(name))
            {
                routes.Add(route);
            }
            else
            {
                routes.Add(name, route);
            }
        }
    }
}