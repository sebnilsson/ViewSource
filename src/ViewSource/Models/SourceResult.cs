using System;
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

namespace ViewSource.Models
{
    public class SourceResult
    {
        public SourceResult(string url, string source, int size = 0)
        {
            this.Url = url;
            this.Source = source;
            ParseLinks(this.Source);

            if (size > 0)
            {
                this.Size = this.GetFileSize(size);
            }
        }

        public string Encoding { get; set; }

        public IEnumerable<Link> ScriptLinks { get; private set; }

        public IEnumerable<Link> Links { get; private set; }

        public string Size { get; private set; }

        public string Source { get; private set; }

        public string Url { get; private set; }

        internal string GetFileSize(int size) {
            if(size >= 1073741824) {
                return (size / 1073741824) + " GB";
            }
            if(size >= 1048576) {
                return (size / 1048576) + " Mb";
            }
            if(size >= 1024) {
                return (size / 1024) + " Kb";
            }
            return size + " bytes";
        }

        private void ParseLinks(string source)
        {
            var document = new HtmlDocument();
            document.LoadHtml(source);

            var scriptLinks = from node in document.DocumentNode.SelectNodes("//script") ?? Enumerable.Empty<HtmlNode>()
                              let src = node.Attributes["src"]
                              let type = node.Attributes["type"]
                              where src != null && !string.IsNullOrWhiteSpace(src.Value)
                              select GetLink(src.Value, type != null ? type.Value : "text/javascript");

            var links = from node in document.DocumentNode.SelectNodes("//link") ?? Enumerable.Empty<HtmlNode>()
                        let href = node.Attributes["href"]
                        let type = node.Attributes["type"]
                        let rel = node.Attributes["rel"]
                        where href != null && !string.IsNullOrWhiteSpace(href.Value)
                        select GetLink(href.Value, type != null ? type.Value : (rel != null ? rel.Value : null));

            this.ScriptLinks = scriptLinks.OrderBy(x => x.Title).ToList();

            this.Links = links.OrderBy(x => x.Title).ToList();
        }

        private Link GetLink(string url, string type)
        {
            url = EnsureAbsoluteUri(url);

            string title = this.GetFriendlyUrlTitle(url);

            return new Link { Title = title, Type = type, Url = new Uri(url, UriKind.Absolute) };
        }

        private string EnsureAbsoluteUri(string url) {
            if(Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                return url;
            }

            var baseUri = new Uri(this.Url);
            var absolute = new Uri(baseUri, url);
            return absolute.ToString();
        }

        private string GetFriendlyUrlTitle(string url)
        {
            var currentHost = new Uri(this.Url).Host;
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);

            string title = url;
            if(uri.IsAbsoluteUri && currentHost.Equals(uri.Host, StringComparison.InvariantCultureIgnoreCase))
            {
                title = uri.LocalPath;
            }

            return title;
        }

        public class Link
        {
            public string Title { get; set; }

            public string Type { get; set; }

            public Uri Url { get; set; }
        }
    }
}