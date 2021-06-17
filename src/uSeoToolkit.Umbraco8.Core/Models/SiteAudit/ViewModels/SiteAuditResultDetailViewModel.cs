using System;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels
{
    public class SiteAuditResultDetailViewModel
    {
        public string Message { get; set; }
        public Guid CheckId { get; set; }
        public bool IsError { get; set; }
        public bool IsWarning { get; set; }
    }
}
