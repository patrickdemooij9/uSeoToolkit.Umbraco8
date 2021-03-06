namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.ViewModels
{
    public class SiteAuditPageDetailViewModel
    {
        public string Url { get; set; }
        public int StatusCode { get; set; }
        public SiteAuditResultDetailViewModel[] Results { get; set; }
    }
}
