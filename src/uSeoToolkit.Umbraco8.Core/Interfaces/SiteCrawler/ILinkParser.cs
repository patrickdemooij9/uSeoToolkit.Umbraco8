using System;
using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler
{
    public interface ILinkParser
    {
        IEnumerable<Uri> GetLinks(CrawledPageModel page);
    }
}
