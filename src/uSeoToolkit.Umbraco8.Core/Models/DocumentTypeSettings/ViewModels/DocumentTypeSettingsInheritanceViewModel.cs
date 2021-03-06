using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels
{
    public class DocumentTypeSettingsInheritanceViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
