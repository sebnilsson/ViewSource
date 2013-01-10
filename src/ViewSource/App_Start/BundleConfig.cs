using SquishIt.Framework;

namespace ViewSource
{
    public static class BundleConfig
    {
        public static void RegisterCachedBundles()
        {
            // SiteStyles
            Bundle.Css()
                  .Add("~/libraries/Normalize.css")
                  .Add("~/libraries/add2home/add2home.css")
                  .Add("~/content/Site.css")
                  .AsCached("bundled-site", "~/assets/css/bundled-site.js");

            // HeadScripts
            //Bundle.JavaScript()
            //    .Add("~/Libraries/Modernizr.2.6.2.custom.js")
            //    .AsCached("bundled-head", "~/assets/js/bundled-head.js");

            // SiteScripts
            Bundle.JavaScript()
                  .Add("~/libraries/beautify-html.js")
                  .Add("~/libraries/add2home/add2home.js")
                  .Add("~/content/Site.js")
                  .AsCached("bundled-site", "~/assets/js/bundled-site.js");
        }
    }
}