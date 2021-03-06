using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class OpenGraphDescriptionField : ISeoField
    {
        public string Title => "Open Graph Description";
        public string Alias => SeoFieldAliasConstants.OpenGraphDescription;
        public string Description => "Description for Open Graph";
        public string View => "Custom";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", new[] { "Umbraco.TextBox", "Umbraco.TextArea" }}
        };
    }
}
