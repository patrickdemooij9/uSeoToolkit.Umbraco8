using Newtonsoft.Json;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldPreviewers
{
    public class FieldPreviewerViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }

        public FieldPreviewerViewModel(ISeoFieldPreviewer model)
        {
            Title = model.Title;
            View = model.View;
        }
    }
}
