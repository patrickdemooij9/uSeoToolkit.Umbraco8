using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Core.Services.SeoValueService;

namespace uSeoToolkit.Umbraco8.Core.Common.Providers
{
    public class DefaultMetaTagsProvider : IMetaTagsProvider
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;
        private readonly ISeoFieldCollection _seoFieldCollection;
        private readonly SeoValueService _seoValueService;

        public DefaultMetaTagsProvider(IDocumentTypeSettingsService documentTypeSettingsService,
            ISeoFieldCollection seoFieldCollection,
            SeoValueService seoValueService)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
            _seoFieldCollection = seoFieldCollection;
            _seoValueService = seoValueService;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            var settings = _documentTypeSettingsService.Get(content.ContentType.Id);
            if (settings?.EnableSeoSettings != true)
                return null;
            var userValues = _seoValueService.GetUserValues(content.Id);
            var fields = _seoFieldCollection.GetAll().Select(it =>
            {
                if (userValues.ContainsKey(it.Alias) && !it.EditEditor.ValueConverter.IsEmpty(userValues[it.Alias]))
                {
                    return new SeoValue(it.Alias, userValues[it.Alias]) { IsUserValue = true };
                }

                return new SeoValue(it.Alias, settings.Get(it.Alias));
            }).ToArray();
            if (settings.Inheritance != null)
            {
                var inheritance = settings.Inheritance;
                while (inheritance != null)
                {
                    var inheritedSettings = _documentTypeSettingsService.Get(inheritance.Id);
                    foreach (var (key, _) in inheritedSettings.Fields)
                    {
                        var field = fields.FirstOrDefault(it => it.FieldAlias == key.Alias);
                        if (field is null || field.IsUserValue)
                            continue;

                        field.Value = key.Editor.Inherit(field.Value, inheritedSettings.Get(key.Alias));
                    }

                    inheritance = settings.Inheritance;
                }
            }

            return new MetaTagsModel(fields.ToDictionary(it => _seoFieldCollection.Get(it.FieldAlias), it => it.IsUserValue ? _seoFieldCollection.Get(it.FieldAlias).EditEditor.ValueConverter.ConvertDatabaseToSeoValue(it.Value) : _seoFieldCollection.Get(it.FieldAlias).Editor.GetValue(content, it.Value)));
        }
    }
}
