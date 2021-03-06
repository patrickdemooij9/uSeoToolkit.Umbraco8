using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class OpenGraphTitleField : ISeoField
    {
        public string Title => "Open Graph Title";
        public string Alias => SeoFieldAliasConstants.OpenGraphTitle;
        public string Description => "Title for open graph";
        public string View => "Custom";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", new[] { "Umbraco.TextBox", "Umbraco.TextArea" }}
        };
    }
}
