using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.ContentEditing;
using Umbraco.Core.Models.Membership;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;

namespace uSeoToolkit.Umbraco8.Core.Common.ContentApps
{
    public class USeoToolkitSeoSettingsAppFactory : IContentAppFactory
    {
        private readonly IDocumentTypeSettingsService _documentTypeSettingsService;

        public USeoToolkitSeoSettingsAppFactory(IDocumentTypeSettingsService documentTypeSettingsService)
        {
            _documentTypeSettingsService = documentTypeSettingsService;
        }

        public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            if (!(source is IContent content) || !_documentTypeSettingsService.IsEnabled(content)) return null;

            return new ContentApp
            {
                Name = "Seo",
                Alias = "seoSettings",
                Icon = "icon-globe-alt",
                Weight = 100,
                View = "/App_Plugins/uSeoToolkit/Interface/ContentApps/SeoSettings/seoSettings.html"
            };
        }
    }
}
