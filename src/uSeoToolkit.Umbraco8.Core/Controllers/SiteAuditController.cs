using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using uSeoToolkit.Umbraco8.Core.Common.Hubs;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.PostModels;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels;
using uSeoToolkit.Umbraco8.Core.Services.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [PluginController("uSeoToolkit")]
    public class SiteAuditController : UmbracoAuthorizedApiController
    {
        private readonly SiteAuditService _siteAuditService;
        private readonly ISiteCheckCollection _siteCheckCollection;
        private readonly SiteAuditHubClientService _siteAuditHubClient;

        public SiteAuditController(SiteAuditService siteAuditService, ISiteCheckCollection siteCheckCollection, SiteAuditHubClientService siteAuditHubClient)
        {
            _siteAuditService = siteAuditService;
            _siteCheckCollection = siteCheckCollection;
            _siteAuditHubClient = siteAuditHubClient;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var model = _siteAuditService.Get(id);
            if (model is null)
                return NotFound();

            return Json(new SiteAuditDetailViewModel(model));
        }

        //SignalR stuff
        [HttpGet]
        public IHttpActionResult Connect(int auditId, string clientId)
        {
            _siteAuditHubClient.AssignClient(clientId, auditId);
            return Get(auditId);
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
                SiteChecks = _siteCheckCollection.GetAll().Where(it => postModel.Checks.Contains(it.Id)).ToList(),
                MaxPagesToCrawl = postModel.MaxPagesToCrawl,
                DelayBetweenRequests = postModel.DelayBetweenRequests
            };
            model = _siteAuditService.Save(model);
            if (postModel.StartAudit)
            {
                Task.Run(() =>
                {
                    try
                    {
                        var result = _siteAuditService.StartSiteAudit(model).Result;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(typeof(SiteAuditController), ex);
                    }
                });
            }
            return Ok(model.Id);
        }
    }
}
