using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
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
        private readonly ISeoConverterCollection _seoConverterCollection;
        private readonly ILogger _logger;

        public DefaultMetaTagsProvider(IDocumentTypeSettingsService documentTypeSettingsService,
            ISeoFieldCollection seoFieldCollection,
            SeoValueService seoValueService,
            ISeoConverterCollection seoConverterCollection,
            ILogger logger)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
            _seoFieldCollection = seoFieldCollection;
            _seoValueService = seoValueService;
            _seoConverterCollection = seoConverterCollection;
            _logger = logger;
        }

        public MetaTagsModel Get(IPublishedContent content)
        {
            var settings = _documentTypeSettingsService.Get(content.ContentType.Id);
            if (settings?.EnableSeoSettings != true)
                return null;
            var userValues = _seoValueService.GetUserValues(content.Id);
            var fields = _seoFieldCollection.GetAll().Select(it =>
            {
                object intermediateObject = null;
                if (userValues.ContainsKey(it.Alias))
                {
                    var result = it.EditEditor.ValueConverter.ConvertDatabaseToObject(userValues[it.Alias]);
                    if (!it.EditEditor.ValueConverter.IsEmpty(result))
                        intermediateObject = result;
                }

                if (intermediateObject is null)
                {
                    intermediateObject = settings.Get(it.Alias);
                }

                if (intermediateObject is null)
                    return new SeoValue(it, null);
                var converter = _seoConverterCollection.GetConverter(intermediateObject.GetType(), it.FieldType);
                if (!(converter is null))
                    return new SeoValue(it, converter.Convert(intermediateObject, content));

                _logger.Warn(GetType(), "No converter found for conversion {0} to {1}", intermediateObject.GetType(), it.FieldType);
                return new SeoValue(it, intermediateObject);
            }).ToArray();

            //TODO: Redo inheritance
            /*if (settings.Inheritance != null)
            {
                var inheritance = settings.Inheritance;
                while (inheritance != null)
                {
                    var inheritedSettings = _documentTypeSettingsService.Get(inheritance.Id);
                    foreach (var (key, _) in inheritedSettings.Fields)
                    {
                        var field = fields.FirstOrDefault(it => it.Field == key);
                        if (field is null || field.IsUserValue)
                            continue;

                        field.Value = key.Editor.Inherit(field.Value, inheritedSettings.Get(key.Alias));
                    }

                    inheritance = settings.Inheritance;
                }
            }*/

            return new MetaTagsModel(fields.ToDictionary(it => it.Field, it => it.Value));
        }
    }
}
