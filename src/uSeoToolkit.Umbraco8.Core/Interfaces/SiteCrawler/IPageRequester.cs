using System;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Models.SiteCrawler;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler
{
    public interface IPageRequester
    {
        Task<CrawledPageModel> MakeRequest(Uri uri);
    }
}
