using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business
{
    public class SiteAuditDto : IEntityWithIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri StartingUrl { get; set; }
        public int? MaxPagesToCrawl { get; set; }
        public int DelayBetweenRequests { get; set; }
        public List<ISiteCheck> SiteChecks { get; set; }
        public ConcurrentQueue<CrawledPageDto> CrawledPages { get; set; }

        public SiteAuditDto()
        {
            SiteChecks = new List<ISiteCheck>();
            CrawledPages = new ConcurrentQueue<CrawledPageDto>();
        }

        public void AddPage(CrawledPageDto page)
        {
            if (CrawledPages.Contains(page))
                throw new ApplicationException($"Page with url {page.PageUrl} is already crawled!");
            CrawledPages.Enqueue(page);
        }
    }
}
