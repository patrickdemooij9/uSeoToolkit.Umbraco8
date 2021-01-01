using System;
using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels
{
    public class DocumentTypeSettingsContentViewModel
    {
        [JsonProperty("enableSeoSettings")] 
        public bool EnableSeoSettings { get; set; }

        [JsonProperty("defaultTitleFields")]
        public string[] DefaultTitleFields { get; set; }

        [JsonProperty("defaultDescriptionFields")]
        public string[] DefaultDescriptionFields { get; set; }

        public DocumentTypeSettingsContentViewModel()
        {
            DefaultTitleFields = Array.Empty<string>();
            DefaultDescriptionFields = Array.Empty<string>();
        }

        public DocumentTypeSettingsContentViewModel(DocumentTypeSettingsDto model)
        {
            EnableSeoSettings = model.EnableSeoSettings;
            DefaultTitleFields = model.DefaultTitleFields;
            DefaultDescriptionFields = model.DefaultDescriptionFields;
        }
    }
}
