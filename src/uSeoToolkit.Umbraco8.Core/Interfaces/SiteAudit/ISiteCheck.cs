using System;
using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit
{
    public interface ISiteCheck
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }

        IEnumerable<PageCrawlResult> RunCheck(CrawledPageModel page);
        string FormatMessage(PageCrawlResult crawlResult);
        bool Compare(PageCrawlResult result, PageCrawlResult otherResult);
    }
}
