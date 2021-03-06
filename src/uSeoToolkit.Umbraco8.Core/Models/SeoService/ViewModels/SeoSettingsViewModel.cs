using System.Collections.Generic;
using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoService.ViewModels
{
    public class SeoSettingsViewModel
    {
        [JsonProperty("defaultTitleFields")]
        public string[] DefaultTitleFields { get; set; }

        [JsonProperty("defaultDescriptionFields")]
        public string[] DefaultDescriptionFields { get; set; }
    }
}
