using System;
using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.ViewModels
{
    public class DocumentTypeSettingsContentViewModel
    {
        [JsonProperty("enableSeoSettings")]
        public bool EnableSeoSettings { get; set; }

        [JsonProperty("values")]
        public SeoFieldViewModel[] Fields { get; set; }

        [JsonProperty("inheritance")]
        public DocumentTypeSettingsInheritanceViewModel Inheritance { get; set; }

        public DocumentTypeSettingsContentViewModel()
        {
            Fields = Array.Empty<SeoFieldViewModel>();
        }

        public DocumentTypeSettingsContentViewModel(SeoFieldViewModel[] fields)
        {
            Fields = fields;
        }

        public DocumentTypeSettingsContentViewModel(DocumentTypeSettingsDto model, SeoFieldViewModel[] fields) : this(fields)
        {
            EnableSeoSettings = model.EnableSeoSettings;
            Inheritance = model.Inheritance is null ? null : new DocumentTypeSettingsInheritanceViewModel
            {
                Id = model.Inheritance.Id,
                Name = model.Inheritance.Name
            };
        }
    }
}
