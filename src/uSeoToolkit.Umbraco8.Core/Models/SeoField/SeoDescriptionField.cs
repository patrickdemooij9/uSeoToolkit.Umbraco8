using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(200)]
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

        public ISeoFieldEditor Editor => new SeoFieldFieldsEditor(new[] { "Umbraco.TextBox", "Umbraco.TextArea" });

        public HtmlString Render(string value)
        {
            return new HtmlString($"<meta name='description' content='{value}'/>");
        }
    }
}
