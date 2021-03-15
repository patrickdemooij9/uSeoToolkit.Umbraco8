using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels
{
    public class SeoSettingsFieldViewModel
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
