using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors
{
    public class SeoTextBoxEditEditor : ISeoFieldEditEditor
    {
        public string View => "Textbox";
        public Dictionary<string, object> Config { get; }
        public ISeoValueConverter ValueConverter { get; }

        public SeoTextBoxEditEditor()
        {
            ValueConverter = new TextSeoValueConverter();
        }
    }
}
