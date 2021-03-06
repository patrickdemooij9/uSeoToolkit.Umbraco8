using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;

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
        public string Value { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }

        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        public SeoFieldViewModel(ISeoField field)
        {
            Alias = field.Alias;
            Title = field.Title;
            Description = field.Description;
            View = field.View;
            Config = field.Config;
        }

        public SeoFieldViewModel(ISeoField field, string value) : this(field)
        {
            Value = value;
        }
    }
}
