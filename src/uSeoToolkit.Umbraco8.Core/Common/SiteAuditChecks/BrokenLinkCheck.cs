using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Common.Enums;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Common.SiteAuditChecks
{
    public class BrokenLinkCheck : ISiteCheck
    {
        //TODO: Probably replace this with a global httpclient
        private readonly HttpClient _httpClient = new HttpClient();

        private const string BrokenLinkUrl = "BrokenLinkUrl";
        private const string BrokenLinkStatusCode = "BrokenLinkStatusCode";

        public Guid Id => Guid.Parse("e3fd929a-632f-4bcc-8802-db64a1d0a7d4");

        public string Name => "Broken Link Check";

        public string Description => "Checks for broken links on your page!";

        public string FormatMessage(PageCrawlResult crawlResult)
        {
            return $"Broken url: {crawlResult.ExtraValues[BrokenLinkUrl]} ({crawlResult.ExtraValues[BrokenLinkStatusCode]})";
        }

        public IEnumerable<PageCrawlResult> RunCheck(CrawledPageModel page)
        {
            if (page.FoundUrls?.Any() != true)
                yield break;

            foreach (var url in page.FoundUrls)
            {
                using (var message = new HttpRequestMessage(HttpMethod.Head, url))
                {
                    var response = _httpClient.SendAsync(message, CancellationToken.None).Result;
                    if (!response.IsSuccessStatusCode)
                        yield return new PageCrawlResult
                        {
                            Check = this,
                            Result = SiteCrawlResultType.Error,
                            ExtraValues = new Dictionary<string, string>
                            {
                                { BrokenLinkUrl, url.ToString() },
                                { BrokenLinkStatusCode, response.StatusCode.ToString() }
                            }
                        };
                }
            }
        }

        public bool Compare(PageCrawlResult result, PageCrawlResult otherResult)
        {
            return result.Check == otherResult.Check &&
                result.ExtraValues[BrokenLinkUrl] == otherResult.ExtraValues[BrokenLinkUrl];
        }
    }
}
