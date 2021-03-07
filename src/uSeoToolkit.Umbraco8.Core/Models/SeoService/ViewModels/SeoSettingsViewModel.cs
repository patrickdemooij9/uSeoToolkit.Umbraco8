using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoService.ViewModels
{
    public class SeoSettingsViewModel
    {
        [JsonProperty("fields")]
        public SeoSettingsFieldViewModel[] Fields { get; set; }
    }
}
