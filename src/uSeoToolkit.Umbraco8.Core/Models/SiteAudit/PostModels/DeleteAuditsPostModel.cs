using Newtonsoft.Json;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.PostModels
{
    public class DeleteAuditsPostModel
    {
        [JsonProperty("ids")]
        public int[] Ids { get; set; }
    }
}
