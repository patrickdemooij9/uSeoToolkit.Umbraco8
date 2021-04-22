using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(300)]
    public class OpenGraphTitleField : ISeoField
    {
        public string Title => "Open Graph Title";
        public string Alias => SeoFieldAliasConstants.OpenGraphTitle;
        public string Description => "Title for open graph";

        public ISeoFieldEditor Editor => new SeoFieldFieldsEditor(new[] { "Umbraco.TextBox", "Umbraco.TextArea" });
        public ISeoFieldEditEditor EditEditor => new SeoTextBoxEditEditor();

        public HtmlString Render(string value)
        {
            return new HtmlString($"<meta property='og:title' content='{value}'/>");
        }
    }
}
