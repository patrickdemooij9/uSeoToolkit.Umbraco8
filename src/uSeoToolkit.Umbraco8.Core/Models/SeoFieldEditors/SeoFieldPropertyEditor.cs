using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldPropertyEditor : ISeoFieldEditor
    {
        private readonly string _propertyView;
        private readonly Func<IPublishedContent, string, string> _getValueTransformation;
        public string View => "/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/PropertyEditor/propertyEditor.html";

        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"view", _propertyView}
        };

        public SeoFieldPropertyEditor(string propertyView, Func<IPublishedContent, string, string> getValueTransformation = null)
        {
            _propertyView = propertyView;
            _getValueTransformation = getValueTransformation;
        }

        public string Inherit(string currentValue, string inheritedValue)
        {
            return currentValue.IfNullOrWhiteSpace(inheritedValue);
        }

        public string GetValue(IPublishedContent content, string value)
        {
            return _getValueTransformation != null ? _getValueTransformation(content, value) : value;
        }
    }
}
