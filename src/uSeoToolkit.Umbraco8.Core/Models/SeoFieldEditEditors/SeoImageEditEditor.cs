using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors
{
    public class SeoImageEditEditor : ISeoFieldEditEditor
    {
        public string View => "MediaPicker";
        public Dictionary<string, object> Config { get; }
        public ISeoValueConverter ValueConverter { get; }

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
