using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Common.Enums;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business
{
    public class PageCrawlResult
    {
        public ISiteCheck Check { get; set; }
        public SiteCrawlResultType Result { get; set; }
        public Dictionary<string, string> ExtraValues { get; set; }
    }
}
