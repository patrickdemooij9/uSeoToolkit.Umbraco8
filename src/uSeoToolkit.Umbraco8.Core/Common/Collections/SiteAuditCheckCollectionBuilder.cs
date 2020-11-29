using SeoToolkit.Core.Interfaces.SiteAudit;
using Umbraco.Core.Composing;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SiteAuditCheckCollectionBuilder : OrderedCollectionBuilderBase<SiteAuditCheckCollectionBuilder, SiteAuditCheckCollection, ISiteCheck>
    {
        protected override SiteAuditCheckCollectionBuilder This => this;
    }
}
