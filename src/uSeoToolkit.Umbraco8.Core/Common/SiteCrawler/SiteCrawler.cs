using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler;
using uSeoToolkit.Umbraco8.Core.Models.EventArgs;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Common.SiteCrawler
{
    public class SiteCrawler : ISiteCrawler
    {
        private readonly IPageRequester _pageRequester;
        private readonly IScheduler _scheduler;
        private readonly ILinkParser _linkParser;

        public event EventHandler<PageCrawlCompleteArgs> OnPageCrawlCompleted;

        public SiteCrawler(
            IPageRequester pageRequester,
            IScheduler scheduler,
            ILinkParser linkParser)
        {
            _pageRequester = pageRequester ?? new DefaultPageUrlRequester();
            _scheduler = scheduler ?? new DefaultScheduler();
            _linkParser = linkParser ?? new DefaultLinkParser();
        }

        public async Task Crawl(Uri startingUrl, int maxUrlsToCrawl, int delayBetweenRequests = 0)
        {
            var maxCrawls = maxUrlsToCrawl;
            var currentCrawls = 0;
            var crawlComplete = false;

            _scheduler.Add(startingUrl);

            while (!crawlComplete && currentCrawls < maxCrawls)
            {
                var linksLeftToSchedule = _scheduler.Count;
                if (linksLeftToSchedule > 0)
                {
                    var linkToCrawl = _scheduler.GetNext();
                    if (delayBetweenRequests > 0)
                        Thread.Sleep(TimeSpan.FromMilliseconds(delayBetweenRequests));

                    ProcessPage(await _pageRequester.MakeRequest(linkToCrawl));
                    currentCrawls++;
                }
                else
                {
                    //Finished the crawl
                    crawlComplete = true;
                }
            }
        }

        private void ProcessPage(CrawledPageModel page)
        {
            var links = _linkParser.GetLinks(page).ToArray();
            page.FoundUrls = links;
            foreach (var link in links)
            {
                if (!_scheduler.IsUriKnown(link) && page.Url.Authority == link.Authority)
                {
                    _scheduler.Add(link);
                }
            }
            _scheduler.AddKnownUri(page.Url);
            OnPageCrawlCompleted?.Invoke(this, new PageCrawlCompleteArgs() { Page = page });
        }
    }
}
