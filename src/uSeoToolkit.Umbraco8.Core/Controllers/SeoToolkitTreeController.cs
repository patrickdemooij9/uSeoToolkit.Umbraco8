using System.Net.Http.Formatting;
using Umbraco.Web;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Controllers
{
    [Tree("uSeoToolkit", TreeControllerConstants.SeoToolkitTreeControllerAlias, TreeTitle = "uSeoToolkit", SortOrder = 1)]
    [PluginController("uSeoToolkit")]
    public class SeoToolkitTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            var node = CreateTreeNode("siteAudit", "-1", queryStrings, "Site Audits", "icon-document", false,
                "uSeoToolkit/SiteAudit/list");
            nodes.Add(node);

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menuItemCollection = new MenuItemCollection();
            if (id == "siteAudit")
            {
                var item = menuItemCollection.Items.Add<ActionNew>(Services.TextService, opensDialog: true);
                item.NavigateToRoute($"{queryStrings.GetRequiredValue<string>("application")}/SiteAudit/create");
            }

            return menuItemCollection;
        }
    }
}
