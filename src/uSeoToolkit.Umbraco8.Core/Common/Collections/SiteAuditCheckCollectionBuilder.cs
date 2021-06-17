using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SiteAuditCheckCollectionBuilder : OrderedCollectionBuilderBase<SiteAuditCheckCollectionBuilder, SiteAuditCheckCollection, ISiteCheck>
    {
        protected override SiteAuditCheckCollectionBuilder This => this;
    }
}
