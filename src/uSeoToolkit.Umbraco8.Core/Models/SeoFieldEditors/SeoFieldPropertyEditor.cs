using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldPropertyEditor : ISeoFieldEditor
    {
        public string View => "~/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/PropertyEditor/propertyEditor.html";
        public Dictionary<string, object> Config => new Dictionary<string, object>();
    }
}
