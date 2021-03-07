using System;
using System.Linq;
using ClientDependency.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Common.Providers
{
    public class DefaultMetaTagsProvider : IMetaTagsProvider
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;
        private readonly ISeoFieldCollection _seoFieldCollection;

        public DefaultMetaTagsProvider(IDocumentTypeSettingsService documentTypeSettingsService,
            ISeoFieldCollection seoFieldCollection)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
            _seoFieldCollection = seoFieldCollection;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            var settings = _documentTypeSettingsService.Get(content.ContentType.Id);
            if (settings?.EnableSeoSettings != true)
                return null;
            return new MetaTagsModel(_seoFieldCollection.GetAll().ToDictionary(it => it, it => GetValue(content, settings.Get(it.Alias).Split(','))));
        }

        public string GetValue(IPublishedContent content, string[] aliasses)
        {
            if (aliasses is null) return null;
            foreach (var alias in aliasses)
            {
                var value = content.Value<string>(alias);
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }

            return null;
        }
    }
}
