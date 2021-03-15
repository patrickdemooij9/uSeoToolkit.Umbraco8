using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoFieldEditor
    {
        string View { get; }
        Dictionary<string, object> Config { get; }

        string Inherit(string currentValue, string inheritedValue);
        string GetValue(IPublishedContent content, string value);
    }
}
