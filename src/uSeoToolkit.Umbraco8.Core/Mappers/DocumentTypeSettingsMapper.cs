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
                    foreach (var item in source.Fields)
                    {
                        var field = _seoFieldCollection.Get(item.Key);
                        if (field is null)
                            continue;

                        target.Fields.Add(field, field.Editor.ValueConverter.ConvertEditorToDatabaseValue(item.Value));
                    }
                    target.Inheritance = source.InheritanceId is null ? null : _contentTypeService.Get(source.InheritanceId.Value);
                });

            mapper.Define<DocumentTypeSettingsEntity, DocumentTypeSettingsDto>(
                (source, context) => new DocumentTypeSettingsDto(),
                (source, target, context) =>
                {
                    target.Content = _contentTypeService.Get(source.NodeId);
                    target.EnableSeoSettings = source.EnableSeoSettings;
                    if (!string.IsNullOrWhiteSpace(source.Fields))
                    {
                        var fields = JsonConvert.DeserializeObject<DocumentTypeFieldEntity[]>(source.Fields);
                        foreach (var item in fields)
                        {
                            var field = _seoFieldCollection.Get(item.Alias);
                            if (field is null)
                                continue;

                            target.Fields.Add(field, field.Editor.ValueConverter.ConvertDatabaseToObject(item.Value));
                        }
                    }
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
