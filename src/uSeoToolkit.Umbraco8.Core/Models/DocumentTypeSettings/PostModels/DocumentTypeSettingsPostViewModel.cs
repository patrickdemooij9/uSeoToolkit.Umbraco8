using System;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.PostModels
{
    public class DocumentTypeSettingsPostViewModel
    {
        [JsonProperty("nodeId")]
        public int NodeId { get; set; }

        [JsonProperty("enableSeoSettings")]
        public bool EnableSeoSettings { get; set; }

        [JsonProperty("defaultTitleFields")]
        public string[] DefaultTitleFields { get; set; }

        [JsonProperty("defaultDescriptionFields")]
        public string[] DefaultDescriptionFields { get; set; }

        public DocumentTypeSettingsPostViewModel()
        {
            DefaultTitleFields = Array.Empty<string>();
            DefaultDescriptionFields = Array.Empty<string>();
        }

        public DocumentTypeSettingsDto ToDto(IContentType type)
        {
            return new DocumentTypeSettingsDto
            {
                Content = type,
                EnableSeoSettings = EnableSeoSettings,
                DefaultTitleFields = DefaultTitleFields,
                DefaultDescriptionFields = DefaultDescriptionFields
            };
        }
    }
}
