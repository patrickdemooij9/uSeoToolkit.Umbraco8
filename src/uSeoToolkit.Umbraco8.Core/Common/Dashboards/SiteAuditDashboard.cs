using Umbraco.Core.Dashboards;

namespace uSeoToolkit.Umbraco8.Core.Common.Dashboards
{
    public class SiteAuditDashboard : IDashboard
    {
        public string[] Sections => new string[] { "uSeoToolkitSection" };

        public IAccessRule[] AccessRules => new IAccessRule[0];

        public string Alias => "siteAuditDashboard";

        public string View => "/App_Plugins/uSeoToolkit/Interface/Dashboards/SiteAudit/siteAuditDashboard.html";
    }
}
