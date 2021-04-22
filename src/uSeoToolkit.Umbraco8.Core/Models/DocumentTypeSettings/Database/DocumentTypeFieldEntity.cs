using Newtonsoft.Json;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database
{
    public class DocumentTypeFieldEntity
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("value")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public object Value { get; set; }
    }
}
