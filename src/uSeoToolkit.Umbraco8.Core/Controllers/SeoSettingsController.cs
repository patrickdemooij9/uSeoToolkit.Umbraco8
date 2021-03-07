using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ClientDependency.Core;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;
using uSeoToolkit.Umbraco8.Core.Models.SeoService.ViewModels;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class SeoSettingsController : UmbracoAuthorizedApiController
    {
        private readonly ISeoService _seoService;
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;
        private readonly ISeoFieldCollection _fieldCollection;

        public SeoSettingsController(ISeoService seoService,
            IDocumentTypeSettingsService documentTypeSettingsService,
            ISeoFieldCollection fieldCollection)
        {
            _seoService = seoService;
            _documentTypeSettingsService = documentTypeSettingsService;
            _fieldCollection = fieldCollection;
        }

        public IHttpActionResult Get(int nodeId, int contentTypeId)
        {
            var documentTypeSettings = _documentTypeSettingsService.Get(contentTypeId);
            var fields = _fieldCollection.GetAll().ToDictionary(it => it.Alias, it => new List<string>());
            if (documentTypeSettings.Inheritance != null)
            {
                var inheritance = documentTypeSettings.Inheritance;
                while (inheritance != null)
                {
                    var settings = _documentTypeSettingsService.Get(inheritance.Id);
                    foreach (var field in settings.Fields)
                    {
                        fields[field.Key].AddRange(field.Value.Split(','));
                    }
                    inheritance = settings.Inheritance;
                }
            }

            foreach (var field in documentTypeSettings.Fields)
            {
                fields[field.Key].AddRange(field.Value.Split(','));
            }
            return Json(new SeoSettingsViewModel
            {
                Fields = fields.Select(it =>
                {
                    var field = _fieldCollection.Get(it.Key);
                    return new SeoSettingsFieldViewModel()
                    {
                        Alias = field.Alias,
                        Title = field.Title,
                        Description = field.Description,
                        Values = it.Value.ToArray()
                    };
                }).ToArray()
            });
        }
    }
}
