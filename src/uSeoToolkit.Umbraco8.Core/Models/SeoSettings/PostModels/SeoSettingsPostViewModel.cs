using System.Collections.Generic;
using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoSettings.PostModels
{
    public class SeoSettingsPostViewModel
    {
        [JsonProperty("nodeId")]
        public int NodeId { get; set; }

        [JsonProperty("contentTypeId")]
        public int ContentTypeId { get; set; }

        [JsonProperty("userValues")]
        public Dictionary<string, object> UserValues { get; set; }
    }
}
