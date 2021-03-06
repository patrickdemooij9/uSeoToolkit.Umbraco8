using System.ComponentModel;
using Umbraco.Core.Mapping;
using Umbraco.Core.Services;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.PostModels;

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
                    target.Fields = source.Fields;
                    target.Inheritance = source.InheritanceId is null ? null : _contentTypeService.Get(source.InheritanceId.Value);
                });
        }
    }
}
