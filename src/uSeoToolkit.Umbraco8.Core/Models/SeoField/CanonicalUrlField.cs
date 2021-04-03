using System.Web;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    [Weight(600)]
    public class CanonicalUrlField : ISeoField
    {
        public string Title => "Canonical Url";
        public string Alias => SeoFieldAliasConstants.CanonicalUrl;
        public string Description => "Canonical Url for the content";
        public ISeoFieldEditor Editor { get; set; }

        public CanonicalUrlField()
        {
            var propertyEditor = new SeoFieldPropertyEditor("textbox", GetEditorValueTransformation);

            Editor = propertyEditor;
        }

        private static string GetEditorValueTransformation(IPublishedContent content, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return value.Replace("%CurrentUrl%", content.Url(mode: UrlMode.Absolute));
        }

        public HtmlString Render(string value)
        {
            return new HtmlString($"<link rel='canonical' href='{value}'/>");
        }
    }
}
