using System;
using System.Linq;
using System.Web.Http;
using SeoToolkit.Core.Interfaces.SiteAudit;
using SeoToolkit.Core.Models.SiteAudit;
using SeoToolkit.Core.Services;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.PostModels;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class SiteAuditController : UmbracoAuthorizedApiController
    {
        private readonly SiteAuditService _siteAuditService;
        private readonly ISiteCheckCollection _siteCheckCollection;

        public SiteAuditController(SiteAuditService siteAuditService, ISiteCheckCollection siteCheckCollection)
        {
            _siteAuditService = siteAuditService;
            _siteCheckCollection = siteCheckCollection;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Json(_siteAuditService.Get(id));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_siteAuditService.GetAll());
        }

        [HttpGet]
        public IHttpActionResult GetAllChecks()
        {
            return Json(_siteCheckCollection.GetAll().Select(it => new SiteAuditCheckViewModel { Id = it.Id, Name = it.Name, Description = it.Description }));
        }

        [HttpPost]
        public IHttpActionResult CreateAudit([FromBody] CreateAuditPostModel postModel)
        {
            var model = new SiteAuditDto
            {
                Name = postModel.Name,
                StartingUrl = new Uri(Umbraco.Content(postModel.SelectedNodeId).Url(mode: UrlMode.Absolute)),
                SiteChecks = _siteCheckCollection.GetAll().Where(it => postModel.Checks.Contains(it.Id)).ToList()
            };
            _siteAuditService.Save(model);
            if (postModel.StartAudit)
            {
                _siteAuditService.StartSiteAudit(model);
            }
            return Ok();
        }
    }
}
