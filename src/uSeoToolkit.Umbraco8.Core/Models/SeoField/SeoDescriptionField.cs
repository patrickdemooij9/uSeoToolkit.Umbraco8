using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class SeoDescriptionField : ISeoField
    {
        public string Title => "Meta Description";
        public string Alias => SeoFieldAliasConstants.MetaDescription;
        public string Description => "Meta description for the page";
        public string View => "Custom";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", new[] { "Umbraco.TextBox", "Umbraco.TextArea" }}
        };
    }
}
