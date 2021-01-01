using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels
{
    public class DocumentTypeSettingsViewModel
    {
        [JsonProperty("defaultTitleFieldTypes")]
        public string[] DefaultTitleFieldTypes { get; set; }

        [JsonProperty("defaultDescriptionFieldTypes")]
        public string[] DefaultDescriptionFieldTypes { get; set; }

        [JsonProperty("contentModel")]
        public DocumentTypeSettingsContentViewModel ContentModel { get; set; }
    }
}
