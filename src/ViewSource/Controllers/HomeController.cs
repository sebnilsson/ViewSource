using System;
using System.Net;
using System.Text;
using System.Web.Mvc;

using ViewSource.Models;

namespace ViewSource.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Result(string url, string encoding)
        {
            string correctedUrl = GetUrl((url ?? string.Empty).ToLowerInvariant());
            encoding = GetEncoding((encoding ?? string.Empty).ToLowerInvariant());

            if(!correctedUrl.Equals(url, StringComparison.InvariantCulture))
            {
                var redirect = Url.RouteUrl("Result", new { url = correctedUrl, encoding });
                return this.RedirectPermanent(redirect);
            }

            string source;
            try
            {
                source = GetSource(url, encoding);
            }
            catch (Exception ex)
            {
                return this.View("Error", (object)ex.Message);
            }

            var result = new SourceResult { Encoding = encoding, Source = source, Url = url };
            return this.View(result);
        }

        private static string GetUrl(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }

            return string.Format("http://{0}", url);
        }

        private static string GetEncoding(string encoding)
        {
            switch (encoding)
            {
                case "ascii":
                    return "ascii";
                case "unicode":
                    return "unicode";
                case "western-europe":
                    return "western-europe";
                default:
                    return "utf8";
            }
        }

        private static string GetSource(string url, string encoding)
        {
            var textEncoding = GetTextEncoding(encoding);

            var client = new WebClient() { Encoding = textEncoding };
            string html = client.DownloadString(url);
            return html;
        }

        private static Encoding GetTextEncoding(string encoding)
        {
            switch (encoding)
            {
                case "ascii":
                    return Encoding.ASCII;
                case "unicode":
                    return Encoding.Unicode;
                case "western-europe":
                    return Encoding.GetEncoding(1252);
                default:
                    return Encoding.UTF8;
            }
        }
    }
}