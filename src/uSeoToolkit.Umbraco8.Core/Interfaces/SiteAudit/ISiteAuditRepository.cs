using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit
{
    public interface ISiteAuditRepository : IRepository<SiteAuditDto>
    {
        void SaveCrawledPage(SiteAuditDto audit, CrawledPageDto page);
    }
}
