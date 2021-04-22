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
    [Weight(500)]
    public class OpenGraphImageField : ISeoField
    {
        public string Title => "Open Graph Image";
        public string Alias => SeoFieldAliasConstants.OpenGraphImage;
        public string Description => "Image for Open Graph";

        public ISeoFieldEditor Editor => new SeoFieldFieldsEditor(new[] { "Umbraco.MediaPicker" });
        public ISeoFieldEditEditor EditEditor => new SeoImageEditEditor();

        public HtmlString Render(string value)
        {
            return new HtmlString($"<meta property='og:image' content='{value}'/>");
        }
    }
}
