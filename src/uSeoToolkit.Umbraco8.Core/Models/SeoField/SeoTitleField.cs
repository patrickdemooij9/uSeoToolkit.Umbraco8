using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class SeoTitleField : ISeoField
    {
        public string Title => "Title";
        public string Alias => SeoFieldAliasConstants.Title;
        public string Description => "Title for the page";
        public string View => "Custom";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", new[] { "Umbraco.TextBox", "Umbraco.TextArea" }}
        };
    }
}
