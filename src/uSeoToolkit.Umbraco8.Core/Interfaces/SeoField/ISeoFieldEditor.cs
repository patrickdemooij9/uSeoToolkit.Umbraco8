using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoFieldEditor
    {
        string View { get; }
        Dictionary<string, object> Config { get; }

        object Inherit(object currentValue, object inheritedValue);
        string GetValue(IPublishedContent content, object value);
    }
}
