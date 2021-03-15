using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;

namespace uSeoToolkit.Umbraco8.Core.Services.SeoService
{
    public class SeoService : ISeoService
    {
        private readonly IMetaTagsProvider _metaTagsProvider;

        public SeoService(IMetaTagsProvider metaTagsProvider)
        {
            _metaTagsProvider = metaTagsProvider;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            return _metaTagsProvider.Get(content);
        }
    }
}
