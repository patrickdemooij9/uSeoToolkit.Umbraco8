using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.Converters
{
    public class FieldsModel
    {
        [JsonProperty("fields")]
        public string[] Fields { get; set; }
    }
}
