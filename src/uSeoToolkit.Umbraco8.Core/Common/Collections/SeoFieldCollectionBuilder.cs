using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SeoFieldCollectionBuilder : WeightedCollectionBuilderBase<SeoFieldCollectionBuilder, SeoFieldCollection, ISeoField>
    {
        protected override SeoFieldCollectionBuilder This => this;
    }
}
