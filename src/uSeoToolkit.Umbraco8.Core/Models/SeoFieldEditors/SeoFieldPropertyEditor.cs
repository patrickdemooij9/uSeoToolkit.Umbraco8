using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldPropertyEditor : ISeoFieldEditor
    {
        public string View => "~/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/PropertyEditor/propertyEditor.html";
        public Dictionary<string, object> Config => new Dictionary<string, object>();
        public string Inherit(string currentValue, string inheritedValue)
        {
            return currentValue.IfNullOrWhiteSpace(inheritedValue);
        }

        public string GetValue(IPublishedContent content, string value)
        {
            return value;
        }
    }
}
