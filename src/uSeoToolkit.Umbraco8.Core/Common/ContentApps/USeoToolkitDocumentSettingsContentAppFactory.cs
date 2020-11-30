using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.ContentEditing;
using Umbraco.Core.Models.Membership;

namespace uSeoToolkit.Umbraco8.Core.Common.ContentApps
{
    public class USeoToolkitDocumentSettingsContentAppFactory : IContentAppFactory
    {
        public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            if (!(source is IContentType))
            {
                return null;
            }

            return new ContentApp()
            {
                Name = "Seo Settings",
                Alias = "seoSettings",
                Icon = "icon-globe-alt",
                Weight = 100
            };
        }
    }
}
