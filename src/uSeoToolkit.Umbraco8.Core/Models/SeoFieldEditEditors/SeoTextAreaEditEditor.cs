using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors
{
    public class SeoTextAreaEditEditor : ISeoFieldEditEditor
    {
        public string View => "Textarea";
        public Dictionary<string, object> Config { get; }
        public IEditorValueConverter ValueConverter { get; }

        public SeoTextAreaEditEditor()
        {
            ValueConverter = new TextValueConverter();
        }
    }
}
