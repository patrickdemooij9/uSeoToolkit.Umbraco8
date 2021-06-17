using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Common.SiteCrawler
{
    public class DefaultScheduler : IScheduler
    {
        private readonly Queue<Uri> _pagesToCrawl;
        private readonly List<Uri> _pagesCrawled;

        public int Count => _pagesToCrawl.Count;

        public DefaultScheduler()
        {
            _pagesToCrawl = new Queue<Uri>();
            _pagesCrawled = new List<Uri>();
        }

        public void Add(Uri pageToCrawl)
        {
            if (IsUriKnown(pageToCrawl)) return;

            _pagesToCrawl.Enqueue(pageToCrawl);
            _pagesCrawled.Add(pageToCrawl);
        }

        public void Add(IEnumerable<Uri> pagesToCrawl)
        {
            foreach (var page in pagesToCrawl)
                Add(page);
        }

        public void AddKnownUri(Uri uri)
        {
            if (!_pagesCrawled.Contains(uri))
                _pagesCrawled.Add(uri);
        }

        public Uri GetNext()
        {
            return _pagesToCrawl.Dequeue();
        }

        public bool IsUriKnown(Uri uri)
        {
            return _pagesCrawled.Contains(uri);
        }
    }
}
