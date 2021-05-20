using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditEditors
{
    public class SeoTextBoxEditEditor : ISeoFieldEditEditor
    {
        public string View => "Textbox";
        public Dictionary<string, object> Config { get; }
        public IEditorValueConverter ValueConverter { get; }

        public SeoTextBoxEditEditor()
        {
            ValueConverter = new TextValueConverter();
        }
    }
}
