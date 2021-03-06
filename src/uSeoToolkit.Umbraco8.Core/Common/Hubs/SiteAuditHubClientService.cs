using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SeoToolkit.Core.Models.SiteAudit;
using SeoToolkit.Core.Services;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels;

namespace uSeoToolkit.Umbraco8.Core.Common.Hubs
{
    public class SiteAuditHubClientService
    {
        private readonly SiteAuditService _siteAuditService;
        private readonly IHubContext _hubContext;

        private readonly Dictionary<string, int> _assignedSiteAudits;

        public SiteAuditHubClientService(SiteAuditService siteAuditService)
        {
            _siteAuditService = siteAuditService;

            _hubContext = GlobalHost.ConnectionManager.GetHubContext<SiteAuditHub>();
            _assignedSiteAudits = new Dictionary<string, int>();

            siteAuditService.OnSiteAuditUpdated += OnSiteAuditUpdatedEventHandler;
        }

        public void AssignClient(string clientId, int auditId)
        {
            if (_assignedSiteAudits.ContainsKey(clientId))
                _assignedSiteAudits[clientId] = auditId;
            else
                _assignedSiteAudits.Add(clientId, auditId);
        }

        public void RemoveClient(string clientId)
        {
            _assignedSiteAudits.Remove(clientId);
        }

        public void Update<T>(string clientId, T model)
        {
            if (_hubContext != null && !string.IsNullOrWhiteSpace(clientId))
            {
                var client = _hubContext.Clients.Client(clientId);
                if (client != null)
                {
                    client.Update(model);
                }
            }
        }

        private void OnSiteAuditUpdatedEventHandler(SiteAuditDto siteAudit)
        {
            if (!_assignedSiteAudits.ContainsValue(siteAudit.Id))
                return;

            var clients = _assignedSiteAudits.Where(it => it.Value == siteAudit.Id);
            _hubContext?.Clients.Clients(clients.Select(it => it.Key).ToList()).Update(new SiteAuditDetailViewModel(siteAudit));
        }
    }
}
