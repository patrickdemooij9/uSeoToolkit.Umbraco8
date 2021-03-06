using System.Linq;
using SeoToolkit.Core.Enums;
using SeoToolkit.Core.Models.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels
{
    public class SiteAuditDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxPagesToCrawl { get; set; }
        public SiteAuditCheckViewModel[] Checks { get; set; }
        public SiteAuditPageDetailViewModel[] PagesCrawled { get; set; }

        public SiteAuditDetailViewModel(SiteAuditDto model)
        {
            //TODO: Move most of this to a mapper

            Id = model.Id;
            Name = model.Name;
            MaxPagesToCrawl = model.MaxPagesToCrawl ?? 0;
            Checks = model.SiteChecks.Select(it => new SiteAuditCheckViewModel
            {
                Id = it.Id,
                Name = it.Name,
                Description = it.Description
            }).ToArray();
            PagesCrawled = model.CrawledPages.Select(it => new SiteAuditPageDetailViewModel
            {
                Url = it.PageUrl.AbsolutePath,
                StatusCode = it.StatusCode,
                Results = it.Results.Select(r => new SiteAuditResultDetailViewModel
                {
                    CheckId = r.Check.Id,
                    Message = r.Check.FormatMessage(r),
                    IsError = r.Result == SiteCrawlResultType.Error,
                    IsWarning = r.Result == SiteCrawlResultType.Warning
                }).ToArray()
            }).ToArray();
        }
    }
}
