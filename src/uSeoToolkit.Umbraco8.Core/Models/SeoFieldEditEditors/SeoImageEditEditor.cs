using System.Collections.Generic;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors
{
    public class SeoImageEditEditor : ISeoFieldEditEditor
    {
        public string View => "MediaPicker";
        public Dictionary<string, object> Config { get; }
        public IEditorValueConverter ValueConverter { get; }

        public SeoImageEditEditor(IUmbracoContextFactory umbracoContextFactory)
        {
            ValueConverter = new UmbracoMediaUdiConverter(umbracoContextFactory);
            Config = new Dictionary<string, object>
            {
                {"disableFolderSelect", false},
                {"idType", "udi"},
                {"ignoreUserStartNodes", false},
                {"multiPicker", false},
                {"onlyImages", true}
            };
        }
    }
}
