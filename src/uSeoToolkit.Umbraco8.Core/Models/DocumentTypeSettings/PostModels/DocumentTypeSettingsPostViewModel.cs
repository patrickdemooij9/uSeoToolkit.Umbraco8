using System;
using System.Collections.Generic;
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

        [JsonProperty("fields")]
        public Dictionary<string, string> Fields { get; set; }

        [JsonProperty("inheritanceId")]
        public int? InheritanceId { get; set; }

        public DocumentTypeSettingsPostViewModel()
        {
            Fields = new Dictionary<string, string>();
        }
    }
}
