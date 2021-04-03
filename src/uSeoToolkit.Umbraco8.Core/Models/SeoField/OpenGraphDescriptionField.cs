using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(400)]
    public class OpenGraphDescriptionField : ISeoField
    {
        public string Title => "Open Graph Description";
        public string Alias => SeoFieldAliasConstants.OpenGraphDescription;
        public string Description => "Description for Open Graph";

        public ISeoFieldEditor Editor => new SeoFieldFieldsEditor(new[] { "Umbraco.TextBox", "Umbraco.TextArea" });

        public HtmlString Render(string value)
        {
            return new HtmlString($"<meta property='og:description' content='{value}'/>");
        }
    }
}
