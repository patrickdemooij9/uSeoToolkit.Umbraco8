using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldPreviewers;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoSettings.ViewModels
{
    public class SeoSettingsViewModel
    {
        [JsonProperty("fields")]
        public SeoSettingsFieldViewModel[] Fields { get; set; }

        [JsonProperty("previewers")]
        public FieldPreviewerViewModel[] Previewers { get; set; }
    }
}
