using System;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Models.EventArgs;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler
{
    public interface ISiteCrawler
    {
        event EventHandler<PageCrawlCompleteArgs> OnPageCrawlCompleted;

        Task Crawl(Uri startingUrl, int maxUrlsToCrawl, int delayBetweenRequests = 0);
    }
}
