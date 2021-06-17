using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler;
using uSeoToolkit.Umbraco8.Core.Models.EventArgs;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;

namespace uSeoToolkit.Umbraco8.Core.Services.SiteAudit
{
    public class SiteAuditService
    {
        public event Action<SiteAuditDto> OnSiteAuditUpdated;

        private readonly ISiteAuditRepository _siteAuditRepository;
        private readonly ISiteCrawler _siteCrawler;

        private SiteAuditDto _currentSiteAudit;

        public SiteAuditService(ISiteAuditRepository siteAuditRepository, ISiteCrawler siteCrawler)
        {
            _siteAuditRepository = siteAuditRepository;
            _siteCrawler = siteCrawler;
        }

        public async Task<SiteAuditDto> StartSiteAudit(SiteAuditDto model)
        {
            _currentSiteAudit = model;

            //TODO: Use a delegate here as this won't work with multiple requests now
            _siteCrawler.OnPageCrawlCompleted += HandleChecks;
            await _siteCrawler.Crawl(model.StartingUrl, model.MaxPagesToCrawl ?? int.MaxValue, model.DelayBetweenRequests);
            _siteCrawler.OnPageCrawlCompleted -= HandleChecks;
            return model;
        }

        public SiteAuditDto Save(SiteAuditDto model)
        {
            return model.Id == 0 ? _siteAuditRepository.Add(model) : _siteAuditRepository.Update(model);
        }

        public SiteAuditDto Get(int id) => _siteAuditRepository.Get(id);
        public IEnumerable<SiteAuditDto> GetAll() => _siteAuditRepository.GetAll();
        public void Delete(int id) => _siteAuditRepository.Delete(id);

        private void HandleChecks(object sender, PageCrawlCompleteArgs args)
        {
            var crawledPage = new CrawledPageDto
            {
                PageUrl = args.Page.Url,
                StatusCode = args.Page.StatusCode
            };
            crawledPage.Results.AddRange(_currentSiteAudit.SiteChecks?.SelectMany(it => it.RunCheck(args.Page)) ?? Enumerable.Empty<PageCrawlResult>());

            _siteAuditRepository.SaveCrawledPage(_currentSiteAudit, crawledPage);
            _currentSiteAudit.AddPage(crawledPage);

            OnSiteAuditUpdated?.Invoke(_currentSiteAudit);
        }
    }
}
