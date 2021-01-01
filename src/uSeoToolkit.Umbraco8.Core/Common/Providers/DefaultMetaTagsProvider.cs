using System;
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

        public DefaultMetaTagsProvider(IDocumentTypeSettingsService documentTypeSettingsService)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            var settings = _documentTypeSettingsService.Get(content.ContentType.Id);
            if (settings?.EnableSeoSettings != true)
                return null;
            return new MetaTagsModel
            {
                Title = GetValue(content, settings.DefaultTitleFields),
                Description = GetValue(content, settings.DefaultDescriptionFields)
            };
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
