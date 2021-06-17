using System;
using HtmlAgilityPack;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteCrawler
{
    public class CrawledPageModel
    {
        public Uri Url { get; }
        public Uri[] FoundUrls { get; set; }
        public DateTime RequestStarted { get; set; }
        public DateTime RequestCompleted { get; set; }
        public int StatusCode { get; set; }
        public HtmlDocument Content { get; set; }

        public CrawledPageModel(Uri url)
        {
            Url = url;
        }
    }
}
