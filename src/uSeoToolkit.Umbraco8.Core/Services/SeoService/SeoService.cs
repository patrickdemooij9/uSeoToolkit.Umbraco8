using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Services.SeoService
{
    public class SeoService
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;
        private readonly IMetaTagsProvider _metaTagsProvider;

        public SeoService(IDocumentTypeSettingsService documentTypeSettingsService, IMetaTagsProvider metaTagsProvider)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
            _metaTagsProvider = metaTagsProvider;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            return _metaTagsProvider.Get(content);
        }
    }
}
