using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Tests.Mocks
{
    public class SeoFieldEditorMock : ISeoFieldEditor
    {
        private readonly Func<string, string, string> _inheritFunc;
        private readonly Func<IPublishedContent, string, string> _getValueFunc;
        public string View { get; set; }
        public Dictionary<string, object> Config { get; set; }

        public SeoFieldEditorMock(Func<string, string, string> inheritFunc = null, Func<IPublishedContent, string, string> getValueFunc = null)
        {
            _inheritFunc = inheritFunc;
            _getValueFunc = getValueFunc;
        }

        public string Inherit(string currentValue, string inheritedValue)
        {
            return _inheritFunc?.Invoke(currentValue, inheritedValue);
        }

        public string GetValue(IPublishedContent content, string value)
        {
            return _getValueFunc?.Invoke(content, value);
        }
    }
}
