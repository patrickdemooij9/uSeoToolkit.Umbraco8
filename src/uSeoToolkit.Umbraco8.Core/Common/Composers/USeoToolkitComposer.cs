using SeoToolkit.Core.Common.SiteAuditChecks;
using SeoToolkit.Core.Common.SiteCrawler;
using SeoToolkit.Core.Interfaces.SiteAudit;
using SeoToolkit.Core.Interfaces.SiteCrawler;
using SeoToolkit.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Common.Collections;
using uSeoToolkit.Umbraco8.Core.Common.Components;
using uSeoToolkit.Umbraco8.Core.Common.ContentApps;
using uSeoToolkit.Umbraco8.Core.Common.Dashboards;
using uSeoToolkit.Umbraco8.Core.Common.Sections;
using uSeoToolkit.Umbraco8.Core.Repositories;

namespace uSeoToolkit.Umbraco8.Core.Common.Composers
{
    public class USeoToolkitComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<USeoToolkitDatabaseComponent>();

            composition.Sections().Append<USeoToolkitSection>();

            composition.Dashboards().Add<SiteAuditDashboard>();

            composition.ContentApps().Append<USeoToolkitDocumentSettingsContentAppFactory>();

            composition.Register(typeof(ISiteAuditRepository), typeof(SiteAuditDatabaseRepository));
            composition.Register(typeof(ISiteCrawler), typeof(SiteCrawler));
            composition.Register(typeof(IPageRequester), typeof(DefaultPageUrlRequester));
            composition.Register(typeof(IScheduler), typeof(DefaultScheduler));
            composition.Register(typeof(ILinkParser), typeof(DefaultLinkParser));
            composition.Register(typeof(SiteAuditService), typeof(SiteAuditService));
            composition.Register(typeof(ISiteCheckCollection), typeof(SiteAuditCheckCollection));

            composition.WithCollectionBuilder<SiteAuditCheckCollectionBuilder>()
                .Append<BrokenLinkCheck>();
        }
    }
}
