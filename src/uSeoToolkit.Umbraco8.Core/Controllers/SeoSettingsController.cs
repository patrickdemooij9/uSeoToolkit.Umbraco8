using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ClientDependency.Core;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldPreviewers;
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
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public SeoSettingsController(ISeoService seoService,
            IDocumentTypeSettingsService documentTypeSettingsService,
            ISeoFieldCollection fieldCollection,
            IUmbracoContextFactory umbracoContextFactory)
        {
            _seoService = seoService;
            _documentTypeSettingsService = documentTypeSettingsService;
            _fieldCollection = fieldCollection;
            _umbracoContextFactory = umbracoContextFactory;
        }

        public IHttpActionResult Get(int nodeId, int contentTypeId)
        {
            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var content = ctx.UmbracoContext.Content.GetById(true, nodeId);
                if (content is null)
                    return NotFound();

                var metaTags = _seoService.Get(content);

                return Json(new SeoSettingsViewModel
                {
                    Fields = metaTags.Fields.Select(it => new SeoSettingsFieldViewModel
                    {
                        Alias = it.Key.Alias,
                        Title = it.Key.Title,
                        Description = it.Key.Description,
                        Value = it.Value
                    }).ToArray(),
                    Previewers = new[] { new FieldPreviewerViewModel(new BaseTagsFieldPreviewer()) }
                });
            }
        }
    }
}
