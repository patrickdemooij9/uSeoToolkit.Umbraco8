using System;
using System.Linq;
using ClientDependency.Core;
using Lucene.Net.Search;
using Umbraco.Core;
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
            var fields = _seoFieldCollection.GetAll().ToDictionary(it => it.Alias, it => settings.Get(it.Alias));
            if (settings.Inheritance != null)
            {
                var inheritance = settings.Inheritance;
                while (inheritance != null)
                {
                    var inheritedSettings = _documentTypeSettingsService.Get(inheritance.Id);
                    foreach (var (key, _) in inheritedSettings.Fields)
                    {
                        fields[key.Alias] = key.Editor.Inherit(fields[key.Alias], inheritedSettings.Get(key.Alias));
                    }

                    inheritance = settings.Inheritance;
                }
            }

            return new MetaTagsModel(fields.ToDictionary(it => _seoFieldCollection.Get(it.Key), it => _seoFieldCollection.Get(it.Key).Editor.GetValue(content, it.Value)));
        }
    }
}
