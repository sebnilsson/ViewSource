using SquishIt.Framework;

namespace ViewSource
{
    public static class BundleConfig
    {
        public static void RegisterCachedBundles()
        {
            // SiteStyles
            Bundle.Css()
                  .Add("~/Libraries/Normalize.css")
                  .Add("~/Content/Site.css")
                  .AsCached("bundled-site", "~/assets/css/bundled-site.js");

            // HeadScripts
            //Bundle.JavaScript()
            //    .Add("~/Libraries/Modernizr.2.6.2.custom.js")
            //    .AsCached("bundled-head", "~/assets/js/bundled-head.js");

            // SiteScripts
            Bundle.JavaScript()
                  .Add("~/Libraries/beautify-html.js")
                  .Add("~/Content/Site.js")
                  .AsCached("bundled-site", "~/assets/js/bundled-site.js");
        }
    }
}