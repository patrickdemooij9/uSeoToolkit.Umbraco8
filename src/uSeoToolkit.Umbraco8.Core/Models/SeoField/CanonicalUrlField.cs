using System.Web;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(600)]
    public class CanonicalUrlField : ISeoField
    {
        public string Title => "Canonical Url";
        public string Alias => SeoFieldAliasConstants.CanonicalUrl;
        public string Description => "Canonical Url for the content";
        public ISeoFieldEditor Editor { get; }
        public ISeoFieldEditEditor EditEditor => new SeoTextBoxEditEditor();

        public CanonicalUrlField()
        {
            var propertyEditor = new SeoFieldPropertyEditor("textbox", GetEditorValueTransformation);

            Editor = propertyEditor;
        }

        private static string GetEditorValueTransformation(IPublishedContent content, object value)
        {
            var valueString = value?.ToString();
            if (string.IsNullOrWhiteSpace(valueString))
                return string.Empty;

            return valueString.Replace("%CurrentUrl%", content.Url(mode: UrlMode.Absolute));
        }

        public HtmlString Render(string value)
        {
            return new HtmlString($"<link rel='canonical' href='{value}'/>");
        }
    }
}
