using System;
using Umbraco.Core.Models;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business
{
    public class DocumentTypeSettingsDto
    {
        public IContentType Content { get; set; }
        public bool EnableSeoSettings { get; set; }
        public string[] DefaultTitleFields { get; set; }
        public string[] DefaultDescriptionFields { get; set; }

        public DocumentTypeSettingsDto()
        {
            DefaultTitleFields = Array.Empty<string>();
            DefaultDescriptionFields = Array.Empty<string>();
        }
    }
}
