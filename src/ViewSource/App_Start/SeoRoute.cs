using System;
using System.Web.Routing;

namespace ViewSource
{
    public class SeoRoute : Route
    {
        public SeoRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        public SeoRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public SeoRoute(
            string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public SeoRoute(
            string url,
            RouteValueDictionary defaults,
            RouteValueDictionary constraints,
            RouteValueDictionary dataTokens,
            IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var lowerCaseValues = new RouteValueDictionary();
            foreach (var value in values)
            {
                switch (value.Key.ToLowerInvariant())
                {
                    case "action":
                    case "area":
                    case "controller":
                        lowerCaseValues[value.Key] = Convert.ToString(value.Value).ToLowerInvariant();
                        break;
                    default:
                        lowerCaseValues[value.Key.ToLowerInvariant()] = value.Value;
                        break;
                }
            }

            var pathData = base.GetVirtualPath(requestContext, lowerCaseValues);

            if (pathData != null && !string.IsNullOrWhiteSpace(pathData.VirtualPath)
                && !pathData.VirtualPath.StartsWith("?"))
            {
                string virtualPath = pathData.VirtualPath;
                if (virtualPath.Contains("?") && !virtualPath.Contains("/?"))
                {
                    pathData.VirtualPath = virtualPath.Replace("?", "/?");
                }
                else
                {
                    pathData.VirtualPath += "/";
                }
            }

            return pathData;
        }
    }
}