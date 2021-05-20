using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Tests.Mocks
{
    public class SeoFieldEditorMock : ISeoFieldEditor
    {
        private readonly Func<object, object, object> _inheritFunc;
        private readonly Func<IPublishedContent, object, string> _getValueFunc;
        public string View { get; set; }
        public Dictionary<string, object> Config { get; set; }
        public IEditorValueConverter ValueConverter { get; set; }

        public SeoFieldEditorMock(Func<object, object, object> inheritFunc = null, Func<IPublishedContent, object, string> getValueFunc = null)
        {
            _inheritFunc = inheritFunc;
            _getValueFunc = getValueFunc;
        }

        public object Inherit(object currentValue, object inheritedValue)
        {
            return _inheritFunc?.Invoke(currentValue, inheritedValue);
        }

        public string GetValue(IPublishedContent content, object value)
        {
            return _getValueFunc?.Invoke(content, value);
        }
    }
}
