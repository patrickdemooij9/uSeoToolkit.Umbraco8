using System;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.PostModels
{
    public class CreateAuditPostModel
    {
        public string Name { get; set; }
        public int SelectedNodeId { get; set; }
        public Guid[] Checks { get; set; }
        public bool StartAudit { get; set; }
        public int MaxPagesToCrawl { get; set; }
        public int DelayBetweenRequests { get; set; }
    }
}
