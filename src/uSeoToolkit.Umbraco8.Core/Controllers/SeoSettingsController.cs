using System;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Models.SeoService.ViewModels;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class SeoSettingsController : UmbracoAuthorizedApiController
    {
        private readonly ISeoService _seoService;
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;

        public SeoSettingsController(ISeoService seoService, IDocumentTypeSettingsService documentTypeSettingsService)
        {
            _seoService = seoService;
            _documentTypeSettingsService = documentTypeSettingsService;
        }

        public IHttpActionResult Get(int nodeId, int contentTypeId)
        {
            var documentTypeSettings = _documentTypeSettingsService.Get(contentTypeId);
            var defaultTitleFields = new List<string>();
            var defaultDescriptionFields = new List<string>();
            if (documentTypeSettings.Inheritance != null)
            {
                var inheritance = documentTypeSettings.Inheritance;
                while (inheritance != null)
                {
                    var settings = _documentTypeSettingsService.Get(inheritance.Id);
                    defaultTitleFields.AddRange(settings.GetDefaultTitleFields());
                    defaultDescriptionFields.AddRange(settings.GetDefaultDescriptionFields());
                    inheritance = settings.Inheritance;
                }
            }
            defaultTitleFields.AddRange(documentTypeSettings.GetDefaultTitleFields());
            defaultDescriptionFields.AddRange(documentTypeSettings.GetDefaultDescriptionFields());
            return Json(new SeoSettingsViewModel
            {
                DefaultTitleFields = defaultTitleFields.ToArray(),
                DefaultDescriptionFields = defaultDescriptionFields.ToArray()
            });
        }
    }
}
