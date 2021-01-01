using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.PostModels;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class DocumentTypeSettingsController : UmbracoAuthorizedApiController
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;

        public DocumentTypeSettingsController(IDocumentTypeSettingsService documentTypeSettingsService)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
        }

        [HttpGet]
        public IHttpActionResult Get(int nodeId)
        {
            var model = _documentTypeSettingsService.Get(nodeId);
            var content = model != null ? new DocumentTypeSettingsContentViewModel(model) : new DocumentTypeSettingsContentViewModel();
            return Json(new DocumentTypeSettingsViewModel
            {
                DefaultTitleFieldTypes = new[] { "Umbraco.TextBox" },
                DefaultDescriptionFieldTypes = new[] { "Umbraco.TextArea" },
                ContentModel = content
            });
        }

        [HttpPost]
        public IHttpActionResult Save(DocumentTypeSettingsPostViewModel postModel)
        {
            _documentTypeSettingsService.Set(postModel.ToDto(Services.ContentTypeService.Get(postModel.NodeId)));
            return Ok();
        }
    }
}
