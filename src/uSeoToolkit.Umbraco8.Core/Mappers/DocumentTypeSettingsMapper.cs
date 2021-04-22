using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ClientDependency.Core;
using Newtonsoft.Json;
using Umbraco.Core.Mapping;
using Umbraco.Core.Services;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.PostModels;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Mappers
{
    public class DocumentTypeSettingsMapper : IMapDefinition
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly ISeoFieldCollection _seoFieldCollection;

        public DocumentTypeSettingsMapper(IContentTypeService contentTypeService, ISeoFieldCollection seoFieldCollection)
        {
            _contentTypeService = contentTypeService;
            _seoFieldCollection = seoFieldCollection;
        }

        public void DefineMaps(UmbracoMapper mapper)
        {
            mapper.Define<DocumentTypeSettingsPostViewModel, DocumentTypeSettingsDto>(
                (source, context) => new DocumentTypeSettingsDto(),
                (source, target, context) =>
                {
                    target.Content = _contentTypeService.Get(source.NodeId);
                    target.EnableSeoSettings = source.EnableSeoSettings;
                    target.Fields = source.Fields.ToDictionary(it => _seoFieldCollection.Get(it.Key), it => it.Value);
                    target.Inheritance = source.InheritanceId is null ? null : _contentTypeService.Get(source.InheritanceId.Value);
                });

            mapper.Define<DocumentTypeSettingsEntity, DocumentTypeSettingsDto>(
                (source, context) => new DocumentTypeSettingsDto(),
                (source, target, context) =>
                {
                    target.Content = _contentTypeService.Get(source.NodeId);
                    target.EnableSeoSettings = source.EnableSeoSettings;
                    target.Fields = string.IsNullOrWhiteSpace(source.Fields) ? new Dictionary<ISeoField, string>() : JsonConvert.DeserializeObject<DocumentTypeFieldEntity[]>(source.Fields).ToDictionary(it => _seoFieldCollection.Get(it.Alias), it => it.Value?.ToString());
                    target.Inheritance = source.InheritanceId is null ? null : _contentTypeService.Get(source.InheritanceId.Value);
                });

            mapper.Define<DocumentTypeSettingsDto, DocumentTypeSettingsEntity>(
                (source, context) => new DocumentTypeSettingsEntity(),
                (source, target, context) =>
                {
                    target.NodeId = source.Content.Id;
                    target.EnableSeoSettings = source.EnableSeoSettings;
                    target.Fields = JsonConvert.SerializeObject(source.Fields?.Select(it => new DocumentTypeFieldEntity
                    {
                        Alias = it.Key.Alias,
                        Value = it.Value
                    }).ToArray() ?? Array.Empty<DocumentTypeFieldEntity>());
                    target.InheritanceId = source.Inheritance?.Id;
                });
        }
    }
}
