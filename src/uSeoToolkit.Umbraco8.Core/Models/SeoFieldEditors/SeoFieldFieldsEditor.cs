using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldFieldsEditor : ISeoFieldEditor
    {
        private readonly string[] _fieldTypes;
        public string View => "/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/FieldsEditor/fieldsEditor.html";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", _fieldTypes}
        };

        public SeoFieldFieldsEditor(string[] fieldTypes)
        {
            _fieldTypes = fieldTypes;
        }

        public string Inherit(string currentValue, string inheritedValue)
        {
            if (string.IsNullOrWhiteSpace(currentValue))
                return inheritedValue;
            if (string.IsNullOrWhiteSpace(inheritedValue))
                return currentValue;

            var inheritedFieldValues = inheritedValue.Split(',').ToList();
            inheritedFieldValues.AddRange(currentValue.Split(','));
            return string.Join(",", inheritedFieldValues);
        }

        public string GetValue(IPublishedContent content, string value)
        {
            var aliases = value?.Split(',');
            if (aliases is null) return null;
            foreach (var alias in aliases)
            {
                var returnValue = content.Value(alias);
                if (returnValue is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
                    return stringValue;
                if (returnValue is IPublishedContent publishedContent)
                    return publishedContent.Url(mode: UrlMode.Absolute);
            }

            return null;
        }
    }
}
