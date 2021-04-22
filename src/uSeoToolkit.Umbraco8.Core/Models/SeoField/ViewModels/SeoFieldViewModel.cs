using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors.ViewModels;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField.ViewModels
{
    public class SeoFieldViewModel
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("editor")]
        public SeoFieldEditorViewModel Editor { get; set; }

        public SeoFieldViewModel(ISeoField field)
        {
            Alias = field.Alias;
            Title = field.Title;
            Description = field.Description;
            Editor = new SeoFieldEditorViewModel(field.Editor);
        }

        public SeoFieldViewModel(ISeoField field, object value) : this(field)
        {
            Value = value;
        }
    }
}
