using System;
using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Common.SiteCrawler
{
    public class DefaultLinkParser : ILinkParser
    {
        public IEnumerable<Uri> GetLinks(CrawledPageModel page)
        {
            if (page is null)
                throw new ArgumentNullException(nameof(page));

            var links = page.Content?.DocumentNode.SelectNodes("//a[@href]");
            if (links is null)
                yield break;

            var baseUri = page.Url;
            foreach (var link in links)
            {
                var hrefValue = link.Attributes["href"].Value;
                yield return new Uri(baseUri, hrefValue);
            }
        }
    }
}
