using System.Linq;
using System.Web.Http;
using Lucene.Net.Search.Function;
using Umbraco.Core;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Common.Collections;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.PostModels;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class DocumentTypeSettingsController : UmbracoAuthorizedApiController
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;
        private readonly ISeoFieldCollection _seoFieldCollection;

        public DocumentTypeSettingsController(IDocumentTypeSettingsService documentTypeSettingsService, ISeoFieldCollection seoFieldCollection)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
            _seoFieldCollection = seoFieldCollection;
        }

        [HttpGet]
        public IHttpActionResult Get(int nodeId)
        {
            var model = _documentTypeSettingsService.Get(nodeId);
            var content = model != null ? 
                new DocumentTypeSettingsContentViewModel(model, _seoFieldCollection.GetAll().Select(it => new SeoFieldViewModel(it, it.Editor.ValueConverter.ConvertObjectToEditorValue(model.Get(it.Alias)))).ToArray()) :
                new DocumentTypeSettingsContentViewModel(_seoFieldCollection.GetAll().Select(it => new SeoFieldViewModel(it)).ToArray());
            return Json(new DocumentTypeSettingsViewModel
            {
                ContentModel = content
            });
        }

        [HttpPost]
        public IHttpActionResult Save(DocumentTypeSettingsPostViewModel postModel)
        {
            _documentTypeSettingsService.Set(Mapper.Map<DocumentTypeSettingsPostViewModel, DocumentTypeSettingsDto>(postModel));
            return Ok();
        }
    }
}
