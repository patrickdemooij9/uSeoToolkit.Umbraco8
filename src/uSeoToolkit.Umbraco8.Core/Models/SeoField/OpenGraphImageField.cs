using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class OpenGraphImageField : ISeoField
    {
        public string Title => "Open Graph Image";
        public string Alias => SeoFieldAliasConstants.OpenGraphImage;
        public string Description => "Image for Open Graph";
        public string View => "Custom";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", new[] { "Umbraco.MediaPicker" }}
        };
    }
}
