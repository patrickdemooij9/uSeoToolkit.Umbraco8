using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(100)]
    public class SeoTitleField : ISeoField
    {
        public string Title => "Title";
        public string Alias => SeoFieldAliasConstants.Title;
        public string Description => "Title for the page";

        public ISeoFieldEditor Editor => new SeoFieldFieldsEditor(new[] { "Umbraco.TextBox", "Umbraco.TextArea" });

        public HtmlString Render(string value)
        {
            return new HtmlString($"<title>{value}</title>");
        }
    }
}
