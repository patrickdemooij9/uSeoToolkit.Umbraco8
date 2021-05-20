using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SeoConverterCollectionBuilder : WeightedCollectionBuilderBase<SeoConverterCollectionBuilder, SeoConverterCollection, ISeoValueConverter>
    {
        protected override SeoConverterCollectionBuilder This => this;
    }
}
