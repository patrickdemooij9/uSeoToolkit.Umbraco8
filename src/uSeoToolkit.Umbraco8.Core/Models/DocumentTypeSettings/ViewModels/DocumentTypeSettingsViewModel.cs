using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels
{
    public class DocumentTypeSettingsViewModel
    {
        [JsonProperty("contentModel")]
        public DocumentTypeSettingsContentViewModel ContentModel { get; set; }
    }
}
